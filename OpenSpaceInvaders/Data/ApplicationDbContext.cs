using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenSpaceInvaders.Models;

namespace OpenSpaceInvaders.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookingModel>()
                .HasOne<DesksModel>(d => d.Desk)
                .WithMany(x => x.Bookings)
                .HasForeignKey(k => k.DeskId);


            builder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(builder);
            builder.Entity<BlobModel>(entity =>
            {
                entity.HasKey(e => e.MediaId)
                    .HasName("PK__UserMedia");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.MediaId)
                    //.ValueGeneratedNever()
                    .ValueGeneratedOnAdd()
                    .HasColumnName("media_id");

                entity.Property(e => e.MediaUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("media_url");

                entity.Property(e => e.MediaFileName)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("media_file_name");

                entity.Property(e => e.MediaFileType)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("media_file_type");

                entity.Property(e => e.DateTimeUploaded)
                    .HasDefaultValueSql("getdate()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMedia)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserMedia_fk_User");
            });

        }

        public DbSet<OpenSpaceInvaders.Models.BookingModel> BookingModel { get; set; }
        public DbSet<OpenSpaceInvaders.Models.DesksModel> DesksModel { get; set; }
        public DbSet<OpenSpaceInvaders.Models.BlobModel> BlobModel { get; set; }



    }
}
