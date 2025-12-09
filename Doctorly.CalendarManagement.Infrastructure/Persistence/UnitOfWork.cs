using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Doctorly.CalendarManagement.Infrastructure.Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppointmentsDbContext _context;
        public IGenericRepository<Appointment> Appointments { get; }
        public IGenericRepository<Attendee> Attendees { get; }
        public IGenericRepository<AppointmentAttendee> AppointmentAttendees { get; }
        public IGenericRepository<Notification> Notifications { get; }


        public UnitOfWork(AppointmentsDbContext context)
        {
            _context = context;
            Appointments = new GenericRepository<Appointment>(_context);
            Attendees = new GenericRepository<Attendee>(_context);
            AppointmentAttendees = new GenericRepository<AppointmentAttendee>(_context);
            Notifications = new GenericRepository<Notification>(_context);

        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
