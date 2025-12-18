using FluentAssertions;
using Moq;
using Doctorly.CalendarManagement.Application.Service;
using Doctorly.CalendarManagement.Domain.Entities;
using Doctorly.CalendarManagement.Domain.Repositories;
using Doctorly.CalendarManagement.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace Doctorly.CalendarManagement.ApplicationTests.Services
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<Appointment>> _appointmentsRepoMock;
        private readonly AppointmentService _service;
        private readonly Mock<ILogger<AppointmentService>> _logger;

        public AppointmentServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _appointmentsRepoMock = new Mock<IGenericRepository<Appointment>>();
            _logger = new Mock<ILogger<AppointmentService>>();
            _unitOfWorkMock.SetupGet(u => u.Appointments).Returns(_appointmentsRepoMock.Object);
            _service = new AppointmentService(_unitOfWorkMock.Object, _logger.Object);
        }

        [Fact]
        public async Task GetEventAsync_WithExistingId_ReturnsAppointment()
        {
            var id = Guid.NewGuid();
            var expected = new Appointment
            {
                Id = id,
                Title = "Test",
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                CreatedAt = DateTime.UtcNow
            };
            _appointmentsRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(expected);

            var result = await _service.GetEventAsync(id);

            result.Should().BeSameAs(expected);
        }

        [Fact]
        public async Task GetAllEventsAsync_ReturnsAllAppointments()
        {
            var list = new List<Appointment>
            {
                new() { Id = Guid.NewGuid(), Title = "A", StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1), CreatedAt = DateTime.UtcNow }
            };
            _appointmentsRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var result = await _service.GetAllEventsAsync();

            result.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task CreateEventAsync_WithValidAppointment_CallsAddAndReturnsAppointment()
        {
            var appt = new Appointment { Id = Guid.NewGuid(), Title = "New", StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1), CreatedAt = DateTime.UtcNow };
            _appointmentsRepoMock.Setup(r => r.AddAsync(appt)).Returns(Task.CompletedTask).Verifiable();

            var result = await _service.CreateEventAsync(appt);

            _appointmentsRepoMock.Verify(r => r.AddAsync(appt), Times.Once);
            result.Should().BeSameAs(appt);
        }

        [Fact]
        public async Task UpdateEventAsync_SetsUpdatedAtAndCallsUpdate()
        {
            var appt = new Appointment { Id = Guid.NewGuid(), Title = "U", StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1), CreatedAt = DateTime.UtcNow };
            _appointmentsRepoMock.Setup(r => r.UpdateAsync(appt)).Returns(Task.CompletedTask).Verifiable();

            await _service.UpdateEventAsync(appt);

            _appointmentsRepoMock.Verify(r => r.UpdateAsync(appt), Times.Once);
            appt.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public async Task DeleteEventAsync_CallsDeleteOnce()
        {
            var id = Guid.NewGuid();
            _appointmentsRepoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask).Verifiable();

            await _service.DeleteEventAsync(id);

            _appointmentsRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}