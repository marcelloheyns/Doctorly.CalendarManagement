using Doctorly.CalendarManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.CalendarManagement.Infrastructure.Persistence
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Appointment> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.Attendees)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }

}
