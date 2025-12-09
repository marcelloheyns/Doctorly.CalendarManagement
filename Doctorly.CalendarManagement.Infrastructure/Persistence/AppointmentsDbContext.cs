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
            modelBuilder.Entity<AppointmentAttendee>()
                .HasKey(aa => new { aa.AppointmentId, aa.AttendeeId });

            modelBuilder.Entity<AppointmentAttendee>()
                .HasOne(aa => aa.Appointment)
                .WithMany(a => a.AppointmentAttendees) 
                .HasForeignKey(aa => aa.AppointmentId);

            modelBuilder.Entity<AppointmentAttendee>()
                .HasOne(aa => aa.Attendee)
                .WithMany(a => a.AppointmentAttendees)
                .HasForeignKey(aa => aa.AttendeeId);
        }


    }


}
