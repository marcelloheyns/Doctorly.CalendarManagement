using Doctorly.CalendarManagement.Domain.Entities;

namespace Doctorly.CalendarManagement.Domain.Repositories
{

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Appointment> Events { get; }
        IGenericRepository<Attendee> Attendees { get; }
        Task<int> CompleteAsync();
    }

}
