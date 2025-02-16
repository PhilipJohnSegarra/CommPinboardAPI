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

            modelBuilder.Entity<Post>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.UserExternalId)
            .HasPrincipalKey(o => o.ExternalId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(b => b.PostExternalId)
            .HasPrincipalKey(o => o.ExternalId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserExternalId)
            .HasPrincipalKey(o => o.ExternalId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PinnedPost>()
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(d => d.PostExternalId)
            .HasPrincipalKey(o => o.ExternalId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PinnedPost>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserExternalId)
            .HasPrincipalKey(o => o.ExternalId)
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}