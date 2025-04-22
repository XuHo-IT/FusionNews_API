using Application.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.DataAccess
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; } //New DB set

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<NewsOfPost> NewsOfPosts { get; set; }
        public DbSet<CommentOfPost> Comments { get; set; }
        public DbSet<ChatbotQuestion> ChatbotQuestions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tag
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");
                entity.HasKey(t => t.TagId);
                entity.Property(t => t.TagId).HasColumnName("tag_id").ValueGeneratedOnAdd();
                entity.Property(t => t.TagName).HasColumnName("name");
            });

            // Post
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");
                entity.HasKey(p => p.PostId);
                entity.Property(p => p.PostId).HasColumnName("post_id").ValueGeneratedOnAdd();
                entity.Property(p => p.Title).HasColumnName("title");
                entity.Property(p => p.Content).HasColumnName("content");
                entity.Property(p => p.NewsOfPostId).HasColumnName("news_of_post_id").IsRequired(false);
                entity.Property(p => p.CreateAt).HasColumnName("create_at");
                entity.Property(p => p.UpdateAt).HasColumnName("update_at");

                entity.HasOne(p => p.NewsOfPost)
                    .WithMany(n => n.Posts) // Relation 1-n
                    .HasForeignKey(p => p.NewsOfPostId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // PostTag
            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.ToTable("post_tag");
                entity.HasKey(pt => new { pt.PostId, pt.TagId });
                entity.Property(pt => pt.PostId).HasColumnName("post_id");
                entity.Property(pt => pt.TagId).HasColumnName("tag_id");

                // => Relation n-n
                entity.HasOne(pt => pt.Post)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(pt => pt.PostId);

                entity.HasOne(pt => pt.Tag)
                    .WithMany(t => t.PostTags)
                    .HasForeignKey(pt => pt.TagId);
            });

            // NewsOfPost
            modelBuilder.Entity<NewsOfPost>(entity =>
            {
                entity.ToTable("news_of_post");

                entity.HasKey(n => n.NewsOfPostId);
                entity.Property(n => n.NewsOfPostId).HasColumnName("news_of_post_id").ValueGeneratedOnAdd();
                entity.Property(n => n.Title).HasColumnName("title");
                entity.Property(n => n.ImageUrl).HasColumnName("image_url");
                entity.Property(n => n.Link).HasColumnName("link");
                entity.Property(n => n.Country).HasColumnName("country");
            });

            // CommentOfPost
            modelBuilder.Entity<CommentOfPost>(entity =>
            {
                entity.ToTable("comment_of_post");
                entity.HasKey(c => c.CommentId);
                entity.Property(c => c.CommentId).HasColumnName("comment_id").ValueGeneratedOnAdd();
                entity.Property(c => c.Content).HasColumnName("content");
                entity.Property(c => c.CreateAt).HasColumnName("create_at");
                entity.Property(c => c.UpdateAt).HasColumnName("update_at");
                entity.Property(c => c.PostId).HasColumnName("post_id");
                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId);
            });

            //ChatbotQuestions
            modelBuilder.Entity<ChatbotQuestion>(entity =>
            {
                entity.ToTable("chatbot_question");
                entity.HasKey(t => t.QuestionId);
                entity.Property(t => t.QuestionId).HasColumnName("question_id").ValueGeneratedOnAdd();
                entity.Property(t => t.Question).HasColumnName("question");
                entity.Property(t => t.Answer).HasColumnName("answer");
                entity.Property(t => t.CreateAt).HasColumnName("create_at");
                entity.Property(t => t.UpdateAt).HasColumnName("update_at");

            });

            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.UserName).HasColumnName("username"); // lưu ý là `UserName` chứ không phải `Username`
                entity.Property(u => u.Email).HasColumnName("email");
                entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
