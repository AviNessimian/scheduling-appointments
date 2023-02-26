using FluentValidation;
using SchedulingAppointments.Domain.Extensions;

namespace SchedulingAppointments.Domain.Commands
{
    public class CreateAppointmentCommand
    {
        public string Name { get; set; }
        public double Date { get; set; }


        public void ValidateAndThrow()
        {
            var validator = new CreateAppointmentValidator();
            validator.ValidateAndThrow(this);
        }
    }

    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(_ => _.Name).NotNull().NotEmpty();
            RuleFor(_ => _.Date.FromUnixTime()).NotNull().NotEmpty().GreaterThan(CommonExtensions.epoch);

        }
    }
}
