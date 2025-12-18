using Doctorly.CalendarManagement.Application.Service;
using Doctorly.CalendarManagement.Domain.Repositories;
using Doctorly.CalendarManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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
