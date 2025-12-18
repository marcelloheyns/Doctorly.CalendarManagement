using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Doctorly.CalendarManagement.Application.Service
{
    public class AppointmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IUnitOfWork unitOfWork, ILogger<AppointmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Appointment?> GetEventAsync(Guid id)
        {
            try
            {
                return await _unitOfWork.Appointments.GetByIdAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment with ID {AppointmentId}", id);
                return new Appointment() ;
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllEventsAsync()
        {
            try
            {
                return await _unitOfWork.Appointments.GetAllAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all appointments");
                return [];
            }
        }

        public async Task<Appointment> CreateEventAsync(Appointment evt)
        {
            try
            {
                await _unitOfWork.Appointments.AddAsync(evt);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                return new Appointment();
            }
            return evt;
        }

        public async Task UpdateEventAsync(Appointment evt)
        {
            try
            {
                evt.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Appointments.UpdateAsync(evt);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment with ID {AppointmentId}", evt.Id);
            }
        }

        public async Task DeleteEventAsync(Guid id)
        {
            try
            {
                await _unitOfWork.Appointments.DeleteAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error deleting appointment with ID {AppointmentId}", id);
            }
        }
    }
}
