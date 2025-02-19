using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommPinboardAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommPinboardAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts{ get; set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<PinnedPost> PinnedPosts{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
            HasIndex(u => u.UserName)
            .IsUnique();

            modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Post>()
            .HasOne(u => u.User)
            .WithMany(p => p.Posts)
            .HasForeignKey(a => a.UserId)
            .HasPrincipalKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
            .HasOne(p => p.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(b => b.PostId)
            .HasPrincipalKey(o => o.PostId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
            .HasOne(p => p.User)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.UserId)
            .HasPrincipalKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PinnedPost>()
            .HasOne(p => p.Post)
            .WithMany()
            .HasForeignKey(d => d.PostId)
            .HasPrincipalKey(o => o.PostId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PinnedPost>()
            .HasOne(p => p.User)
            .WithMany(p => p.PinnedPosts)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}