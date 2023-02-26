using SchedulingAppointments.Domain.Models;
using SchedulingAppointments.Domain.Querys;

namespace SchedulingAppointments.Domain.Contracts
{
    public interface IReadProvidersRepository
    {
        Task<bool> AvailabilityExists(double date);
        Task<IEnumerable<ProviderModel>> GetProviders(ProviderSearchQuery query);
    }
}
