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
        }

        public DbSet<OpenSpaceInvaders.Models.BookingModel> BookingModel { get; set; }
        public DbSet<OpenSpaceInvaders.Models.DesksModel> DesksModel { get; set; }
    }
}
