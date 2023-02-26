using FluentValidation;
using Microsoft.Extensions.Logging;
using SchedulingAppointments.Domain.Contracts;
using SchedulingAppointments.Domain.Extensions;
using SchedulingAppointments.Domain.Models;
using SchedulingAppointments.Domain.Querys;


namespace SchedulingAppointments.Domain.Commands
{
    public interface ICreateAppointment
    {
        Task Handle(CreateAppointmentCommand command);
    }

    public class CreateAppointmentHandler : ICreateAppointment
    {
        private readonly IWriteProvidersRepository writeProvidersRepository;
        private readonly IReadProvidersRepository readProvidersRepository;
        private readonly ILogger<ProviderSearchHandler> logger;
        public CreateAppointmentHandler(
            IWriteProvidersRepository writeProvidersRepository,
            ILogger<ProviderSearchHandler> logger,
            IReadProvidersRepository readProvidersRepository)
        {
            this.writeProvidersRepository=writeProvidersRepository;
            this.logger=logger;
            this.readProvidersRepository=readProvidersRepository;
        }

        public async Task Handle(CreateAppointmentCommand command)
        {
            logger.LogInformation("Handling create appointment command {Command}", command);
            command.ValidateAndThrow();

            //validate If such an availability exist or throw ex....
            var isAvailabile = await readProvidersRepository.AvailabilityExists(command.Date);
            if (!isAvailabile)
            {
                throw new ValidationException($"Date {command.Date.ToString().FromUnixTime().ToString("MM/dd/yyyy hh:mm tt")} selected is not availabile");
            }

            var newAppointment = new AppointmentModel
            {
                Name = command.Name,
                Date = command.Date.ToString()
            };

            await writeProvidersRepository.Create(newAppointment);
        }
    }
}
