using Doctorly.CalendarManagement.Domain.Entities;

namespace Doctorly.CalendarManagement.Domain.Repositories
{

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Attendee> Attendees { get; }
        IGenericRepository<Appointment> Appointments { get; }
        IGenericRepository<AppointmentAttendee> AppointmentAttendees { get; }
        IGenericRepository<Notification> Notifications { get; }
        Task<int> SaveChangesAsync();
    }

}
