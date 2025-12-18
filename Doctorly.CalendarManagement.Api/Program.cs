using Doctorly.CalendarManagement.Application.Service;
using Doctorly.CalendarManagement.Domain.Repositories;
using Doctorly.CalendarManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppointmentsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EventsConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddSingleton<INotificationService>(provider =>
    new NotificationService(
        "marcellolheyns@gmail.com",
        new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("marcellolheyns@gmail.com", ">bzRPB4V[;76pN'a"),
            EnableSsl = true

        }
    ));


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks(pattern: "/ready");
app.MapHealthChecks(pattern: "/ping");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
