using Application.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace FusionNews_API.Data
{
    public class ApplicationDBContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Products { get; set; }
        public DbSet<NewsOfPost> NewsOfPosts { get; set; }
        public DbSet<CommentOfPost> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tag
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");
                entity.HasKey(t => t.TagId);
                entity.Property(t => t.TagId).HasColumnName("tag_id");
                entity.Property(t => t.TagName).HasColumnName("name");
            });

            // Post
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");
                entity.HasKey(p => p.PostId);
                entity.Property(p => p.PostId).HasColumnName("post_id");
                entity.Property(p => p.TagId).HasColumnName("tag_id");
                entity.Property(p => p.Title).HasColumnName("title");
                entity.Property(p => p.Content).HasColumnName("content");
                entity.Property(p => p.NewsOfPostId).HasColumnName("news_of_post_id");
                entity.Property(p => p.CreatedOn).HasColumnName("created_on");
                entity.HasOne(p => p.Tag)
                    .WithOne()
                    .HasForeignKey<Post>(p => p.TagId);
                entity
                    .HasOne(p => p.NewsOfPost)
                    .WithOne(n => n.Post)
                    .HasForeignKey<Post>(p => p.NewsOfPostId);
            });

            // NewsOfPost
            modelBuilder.Entity<NewsOfPost>(entity =>
            {
                entity.ToTable("news_of_post");

                entity.HasKey(n => n.NewsOfPostId);
                entity.Property(n => n.NewsOfPostId).HasColumnName("news_of_post_id");
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
                entity.Property(c => c.CommentId).HasColumnName("comment_id");
                entity.Property(c => c.Content).HasColumnName("content");
                entity.Property(c => c.CreatedOn).HasColumnName("created_on");
                entity.Property(c => c.PostId).HasColumnName("post_id");
                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId);
            });
        }
    }
}
