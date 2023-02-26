using FluentValidation;

namespace SchedulingAppointments.Domain.Extensions
{
    public static class CommonExtensions
    {
        public static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTime(this string unixTime)
        {
            return epoch.AddMilliseconds(double.Parse(unixTime));
        }

        public static DateTime FromUnixTime(this double unixTime)
        {
            return epoch.AddMilliseconds(unixTime);
        }

        public static string ToUnixTimeAsString(this DateTime dateTime)
        {
            return dateTime.ToUnixTime().ToString();
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().Subtract(epoch).TotalMilliseconds;
        }

    }
}
