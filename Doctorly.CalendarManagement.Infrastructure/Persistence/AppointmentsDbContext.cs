using Doctorly.CalendarManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.CalendarManagement.Infrastructure.Persistence
{
    public class AppointmentsDbContext : DbContext
    {
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<AppointmentAttendee> AppointmentAttendees => Set<AppointmentAttendee>();
        public DbSet<Notification> Notifications => Set<Notification>();

        public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for AppointmentAttendee
            modelBuilder.Entity<AppointmentAttendee>()
                .HasKey(aa => new { aa.AppointmentId, aa.AttendeeId });

            // Relationship: Appointment → AppointmentAttendees
            modelBuilder.Entity<AppointmentAttendee>()
                .HasOne(aa => aa.Appointment)
                .WithMany(a => a.AppointmentAttendees) // ✅ Must match collection property in Appointment
                .HasForeignKey(aa => aa.AppointmentId);

            // Relationship: Attendee → AppointmentAttendees
            modelBuilder.Entity<AppointmentAttendee>()
                .HasOne(aa => aa.Attendee)
                .WithMany(a => a.AppointmentAttendees) // ✅ Must match collection property in Attendee
                .HasForeignKey(aa => aa.AttendeeId);
        }


    }


}
