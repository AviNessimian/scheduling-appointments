using FluentValidation;
using SchedulingAppointments.Domain.Extensions;

namespace SchedulingAppointments.Domain.Querys
{
    public class ProviderSearchQuery
    {
        public string Specialty { get; set; }
        public double Date { get; set; }
        public double MinScore { get; set; }

        public void ValidateAndThrow()
        {
            var validator = new ProviderSearchValidator();
            validator.ValidateAndThrow(this);
        }
    }

    public class ProviderSearchValidator : AbstractValidator<ProviderSearchQuery>
    {
        public ProviderSearchValidator()
        {
            RuleFor(_ => _.Specialty).NotNull().NotEmpty();
            RuleFor(_ => _.Date.FromUnixTime()).NotNull().NotEmpty().GreaterThan(CommonExtensions.epoch);
            RuleFor(_ => _.MinScore).LessThan(11).GreaterThan(-1);
        }
    }
}

