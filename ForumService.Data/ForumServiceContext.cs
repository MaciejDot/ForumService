using ForumService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ForumService.Data
{
    public class ForumServiceContext : DbContext
    {
        public ForumServiceContext(DbContextOptions<ForumServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Thumbnail> Thumbnail { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.ToTable("Users", "Security");

                entity.Property(r => r.Id)
                    .HasMaxLength(100);

                entity.Property(r => r.Username)
                    .HasMaxLength(100)
                    .IsRequired(true);

                entity.HasMany(u => u.Threads)
                    .WithOne(t => t.Author)
                    .HasForeignKey(t => t.AuthorId)
                    .HasConstraintName("FK_Thread_Author")
                    .IsRequired();

                entity.HasMany(u => u.Posts)
                    .WithOne(t => t.Author)
                    .HasForeignKey(t => t.AuthorId)
                    .HasConstraintName("FK_Posts_Author")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.ClientCascade);

            });

            modelBuilder.Entity<Thumbnail>(entity => {
                entity.ToTable("Thumbnails", "Forum");

                entity.HasKey(t => t.Id)
                    .HasName("PK_Thumbnail");

                entity.Property(t => t.Content)
                    .HasMaxLength(250 * 1024)
                    .IsRequired();

                entity.HasMany(t => t.Subject)
                    .WithOne(s => s.Thumbnail)
                    .HasForeignKey(s => s.ThumbnailId)
                    .HasConstraintName("FK_Subjects_Thumbnails")
                    .IsRequired();

            });

            modelBuilder.Entity<Subject>(entity => {
                entity.ToTable("Subjects", "Forum");

                entity.HasKey(s => s.Id)
                    .HasName("PK_Subject");

                entity.HasIndex(s => s.ThumbnailId)
                    .HasName("IX_Subjects_ThumbnailId");

                entity.Property(s => s.Title)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.Property(s => s.Descriprion)
                    .HasMaxLength(3000)
                    .IsRequired();

                entity.HasIndex(s => s.Title)
                    .HasName("IX_Subject_SubjectName")
                    .IsUnique();

                entity.HasMany(s => s.Thread)
                    .WithOne(t => t.Subject)
                    .HasForeignKey(t => t.SubjectId)
                    .HasConstraintName("FK_Thread_Subjects")
                    .IsRequired();

            });

            modelBuilder.Entity<Thread>(entity =>
            {
                entity.ToTable("Threads", "Forum");
                entity.HasKey(t => t.Id)
                    .HasName("PK_Thread");

                entity.HasIndex(t => t.SubjectId)
                    .HasName("IX_Thread_SubjectId");

                entity.HasIndex(t => t.Created)
                    .HasName("IX_Thread_Created")
                    .IsUnique();

                entity.Property(t => t.Title)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.Property(t => t.Question)
                    .HasMaxLength(4000)
                    .IsRequired();

                entity.HasMany(t => t.Post)
                    .WithOne(p => p.Thread)
                    .HasForeignKey(p => p.ThreadId)
                    .HasConstraintName("FK_Posts_Thread")
                    .IsRequired();



            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts", "Forum");

                entity.HasKey(p => p.Id)
                    .HasName("PK_Post");

                entity.HasIndex(p => p.Created)
                    .HasName("IX_Post_Created")
                    .IsUnique();

                entity.HasIndex(p => p.ThreadId)
                    .HasName("IX_Posts_ThreadId");

                entity.Property(p => p.Answear)
                    .HasMaxLength(4000);

            });
        
        }
        
    }

}
