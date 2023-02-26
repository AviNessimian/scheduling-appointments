using SchedulingAppointments.Domain.Models;

namespace SchedulingAppointments.Domain.Contracts
{
    public interface IWriteProvidersRepository
    {
        Task Create(AppointmentModel provider);
    }
}
