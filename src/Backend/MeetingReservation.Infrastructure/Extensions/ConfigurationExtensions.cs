using Microsoft.Extensions.Configuration;

namespace MeetingReservation.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsUnitTestEnviroment(this IConfiguration configuration) => configuration.GetValue<bool>("InMemoryTest");

    public static string ConnectionString(this IConfiguration configuration) => configuration.GetConnectionString("DefaultConnection")!;
}
