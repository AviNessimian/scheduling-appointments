using SchedulingAppointments.Domain.Commands;
using SchedulingAppointments.Domain.Contracts;
using SchedulingAppointments.Domain.Querys;
using SchedulingAppointments.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IProviderSearch, ProviderSearchHandler>();
builder.Services.AddSingleton<ICreateAppointment, CreateAppointmentHandler>();
builder.Services.AddSingleton<IReadProvidersRepository, ProvidersInMemoryRepo>();
builder.Services.AddSingleton<IWriteProvidersRepository, ProvidersInMemoryRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
