//using System.Collections.Generic;
//using Application.Entities.Base;
//using Microsoft.EntityFrameworkCore;

//namespace FusionNews_API.Data
//{
//    public class UserDbContext : DbContext
//    {
//        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

//        public DbSet<User> Users { get; set; }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<User>(entity =>
//            {
//                entity.ToTable("users"); // tên bảng viết thường

//                entity.HasKey(u => u.Id);
//                entity.Property(u => u.Id).HasColumnName("id");
//                entity.Property(u => u.Username).HasColumnName("username");
//                entity.Property(u => u.Email).HasColumnName("email");
//                entity.Property(u => u.PasswordHash).HasColumnName("password_hash"); // 👈 QUAN TRỌNG
//            });

//            base.OnModelCreating(modelBuilder);
//        }


//    }
//}
