using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Repositories;

namespace Doctorly.CalendarManagement.Infrastructure.Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IGenericRepository<Appointment> Events { get; }
        public IGenericRepository<Attendee> Attendees { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Events = new GenericRepository<Appointment>(_context);
            Attendees = new GenericRepository<Attendee>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
