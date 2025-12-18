using Doctorly.CalendarManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Doctorly.CalendarManagement.ApplicationTests.Helpers
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // remove existing DbContext registration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppointmentsDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // register in-memory db for testing
                services.AddDbContext<AppointmentsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAppointmentsTest");
                });

                // ensure DB is created
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppointmentsDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            });
        }
    }
}
