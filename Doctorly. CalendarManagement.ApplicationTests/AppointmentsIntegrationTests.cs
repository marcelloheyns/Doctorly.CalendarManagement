using Doctorly.CalendarManagement.ApplicationTests.Helpers;
using Doctorly.CalendarManagement.ApplicationTests.Helpers.Models;
using Doctorly.CalendarManagement.Domain.Entities;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Doctorly.CalendarManagement.ApplicationTests
{
    public class AppointmentsIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AppointmentsIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_WhenNoEvents_ReturnsEmptyList()
        {
            var response = await _client.GetAsync("/api/appointments");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var list = await response.Content.ReadFromJsonAsync<Appointment[]>();
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Fact]
        public async Task Create_PostsAppointment_ReturnsCreatedAndLocation()
        {
            var dto = new CreateAppointmentDto
            {
                Title = "Integration Test",
                Description = "desc",
                StartTime = DateTime.UtcNow.AddHours(1),
                EndTime = DateTime.UtcNow.AddHours(2),
                Attendees = new AttendeeDto
                {
                    Name = "Marcello",
                    Email = "marcellolheyns@gmail.com",
                    IsAttending = true
                }
            };

            var response = await _client.PostAsJsonAsync("/api/appointments", dto);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();

            var created = await response.Content.ReadFromJsonAsync<Appointment>();
            created.Should().NotBeNull();
            created!.Title.Should().Be(dto.Title);

            var getResponse = await _client.GetAsync(response.Headers.Location!);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_WithUnknownId_ReturnsNotFound()
        {
            var id = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/appointments/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
