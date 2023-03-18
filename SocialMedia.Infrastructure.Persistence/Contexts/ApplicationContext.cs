using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Domain.Common;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Friends> Friends { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tables

            modelBuilder.Entity<Publicaciones>()
                .ToTable("Publicaciones");

            modelBuilder.Entity<Comentarios>()
                .ToTable("Comentarios");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Friends>()
                .ToTable("Friends");

            #endregion

            #region "primary keys"
            modelBuilder.Entity<Publicaciones>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Comentarios>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Friends>()
               .HasKey(f => f.Id);
            #endregion

            #region "Relationships"

            modelBuilder.Entity<User>()
            .HasMany<Publicaciones>(user => user.Publicaciones)
            .WithOne(P => P.User)            
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany<Friends>(user => user.Friends)
            .WithOne(P => P.User)
            .HasForeignKey(p => p.IdUser)
            .HasForeignKey(p => p.IdFriend)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Publicaciones>()
                .HasMany<Comentarios>(a => a.Comentarios)
                .WithOne(c => c.Post)
                .HasForeignKey(f => f.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region "Property configurations"

            #region Publicaciones

            modelBuilder.Entity<Publicaciones>().
                Property(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<Publicaciones>().
               Property(p => p.Descripcion)
               .IsRequired();

            modelBuilder.Entity<Publicaciones>().
               Property(p => p.Descripcion)
               .IsRequired();

            #endregion

            #region Comentarios
            modelBuilder.Entity<Comentarios>().
              Property(c => c.Descripcion)
              .IsRequired();

            modelBuilder.Entity<Comentarios>().
             Property(c => c.UserId)
             .IsRequired();

            modelBuilder.Entity<Comentarios>().
            Property(c => c.PostId)
            .IsRequired();
            #endregion

            #region users

            modelBuilder.Entity<User>().
                Property(user => user.Name)
                .IsRequired();

            modelBuilder.Entity<User>().
                Property(user => user.LastName)
                .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Username)
               .IsRequired();

            modelBuilder.Entity<User>()
           .HasIndex(user => user.Username)
           .IsUnique();

            modelBuilder.Entity<User>().
              Property(user => user.Password)
              .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Email)
              .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Phone)
               .IsRequired();

            #endregion

            #region Friends

            modelBuilder.Entity<Friends>().
              Property(f => f.IdUser)
              .IsRequired();

            modelBuilder.Entity<Friends>().
            Property(f => f.IdFriend)
            .IsRequired();
            #endregion

            #endregion

        }

    }
}
