using Newtonsoft.Json;
using SchedulingAppointments.Domain.Contracts;
using SchedulingAppointments.Domain.Extensions;
using SchedulingAppointments.Domain.Models;
using SchedulingAppointments.Domain.Querys;

namespace SchedulingAppointments.Infra
{
    public class ProvidersInMemoryRepo : IReadProvidersRepository, IWriteProvidersRepository
    {
        private static List<ProviderModel> providers;
        private static List<AppointmentModel> appointments;
        public ProvidersInMemoryRepo()
        {
            var mockData = File.ReadAllText(@"C:\Users\Avi\source\repos\fullstack-interview\providers\providers.json");
            providers = JsonConvert.DeserializeObject<List<ProviderModel>>(mockData);
            appointments = new List<AppointmentModel>();
        }

        public Task<bool> AvailabilityExists(double date)
        {
            if (providers.Any(_ => _.AvailableDates.Any(a 
                => a.From.FromUnixTime() < date.FromUnixTime()
                && a.To.FromUnixTime() >= date.FromUnixTime())))    
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task Create(AppointmentModel provider)
        {
            appointments.Add(provider);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ProviderModel>> GetProviders(ProviderSearchQuery query)
        {
            var result = providers.Where(_ => _.Score >= query.MinScore);
            result = result.Where(_ => _.Specialties.Contains(query.Specialty, StringComparer.CurrentCultureIgnoreCase));

            result = result.Where(_ => _.AvailableDates.Any(x
                => x.From.FromUnixTime() <= query.Date.FromUnixTime()
                &&
                x.To.FromUnixTime() >= query.Date.FromUnixTime()));

            return Task.FromResult(result);
        }
    }
}