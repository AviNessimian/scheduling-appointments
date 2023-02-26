namespace SchedulingAppointments.Domain.Models
{
    public class ProviderModel
    {
        public string Name { get; set; }
        public double  Score { get; set; }
        public string[] Specialties { get; set; }
        public List<AvailablityModel> AvailableDates { get; set; }
    }
}
