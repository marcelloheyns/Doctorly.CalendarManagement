using System.ComponentModel.DataAnnotations;

namespace Doctorly.CalendarManagement.Api.Models
{

    public class CreateEventRequest
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}
