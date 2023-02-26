using Microsoft.Extensions.Logging;
using SchedulingAppointments.Domain.Contracts;
using SchedulingAppointments.Domain.Models;

namespace SchedulingAppointments.Domain.Querys
{

    public interface IProviderSearch
    {
        Task<List<ProviderModel>> Handle(ProviderSearchQuery query);
    }

    public class ProviderSearchHandler : IProviderSearch
    {
        private readonly IReadProvidersRepository readProvidersRepository;
        private readonly ILogger<ProviderSearchHandler> logger;
        public ProviderSearchHandler(
            IReadProvidersRepository readProvidersRepository,
            ILogger<ProviderSearchHandler> logger)
        {
            this.readProvidersRepository=readProvidersRepository;
            this.logger=logger;
        }

        public async Task<List<ProviderModel>> Handle(ProviderSearchQuery query)
        {
            logger.LogInformation("Handling provider search query {Query}", query);
            query.ValidateAndThrow();

            var providers = await readProvidersRepository.GetProviders(query);
            return providers.OrderByDescending(_ => _.Score).ToList();
        }
    }
}
