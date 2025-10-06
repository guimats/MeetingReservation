using FluentMigrator.Runner;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Reservation;
using MeetingReservation.Domain.Repositories.Token;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Domain.Security.Cryptography;
using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Infrastructure.DataAccess;
using MeetingReservation.Infrastructure.DataAccess.Repositories;
using MeetingReservation.Infrastructure.Extensions;
using MeetingReservation.Infrastructure.Security.Cryptography;
using MeetingReservation.Infrastructure.Security.Tokens.Access.Generator;
using MeetingReservation.Infrastructure.Security.Tokens.Access.Validator;
using MeetingReservation.Infrastructure.Security.Tokens.Refresh;
using MeetingReservation.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeetingReservation.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordsEncripter(services, configuration);
        AddRepositories(services);
        AddLoggedUser(services);
        AddTokens(services, configuration);

        //validando se está em ambiente de teste
        if (configuration.IsUnitTestEnviroment())
            return;

        AddDbContext(services, configuration);
        AddFluentMigrator(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 42));

        services.AddDbContext<MeetingReservationDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            var connectionString = configuration.ConnectionString();

            options
            .AddMySql5()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("MeetingReservation.Infrastructure")).For.All();
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

        services.AddScoped<ITokenRepository, TokenRepository>();

        services.AddScoped<IReservationWriteOnlyRepository, ReservationRepository>();
        services.AddScoped<IReservationReadOnlyRepository, ReservationRepository>();

        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTime");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(options => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(options => new JwtTokenValidator(signingKey!));
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();


    private static void AddPasswordsEncripter(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter>(options => new BCryptNet());
    }
}
