using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Repositories;

namespace Doctorly.CalendarManagement.Application.Service
{

    public class EventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> CreateEventAsync(string? title, string? description, DateTime start, DateTime end)
        {
            var ev = new Event(title, description, start, end);
            await _unitOfWork.Events.AddAsync(ev);
            await _unitOfWork.CompleteAsync();
            return ev;
        }

        public async Task<IEnumerable<Appointment>> GetAllEventsAsync()
        {
            return await _unitOfWork.Events.GetAllAsync();
        }

        public async Task<Appointment?> GetEventByIdAsync(Guid id)
        {
            return await _unitOfWork.Events.GetByIdAsync(id);
        }

        public async Task UpdateEventAsync(Appointment ev)
        {
            await _unitOfWork.Events.UpdateAsync(ev);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteEventAsync(Guid id)
        {
            await _unitOfWork.Events.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }


}
