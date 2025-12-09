using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Repositories;

namespace Doctorly.CalendarManagement.Application.Service
{
    public class AppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment?> GetEventAsync(Guid id)
        {
            return await _unitOfWork.Appointments.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAllEventsAsync()
        {
            return await _unitOfWork.Appointments.GetAllAsync();
        }

        public async Task<Appointment> CreateEventAsync(Appointment evt)
        {
            await _unitOfWork.Appointments.AddAsync(evt);
            return evt;
        }

        public async Task UpdateEventAsync(Appointment evt)
        {
            evt.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Appointments.UpdateAsync(evt);
        }

        public async Task DeleteEventAsync(Guid id)
        {
            await _unitOfWork.Appointments.DeleteAsync(id);
        }
    }
}
