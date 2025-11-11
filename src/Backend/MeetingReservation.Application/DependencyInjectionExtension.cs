using MeetingReservation.Application.UseCases.Login.DoLogin;
using MeetingReservation.Application.UseCases.Reservation.Delete;
using MeetingReservation.Application.UseCases.Reservation.GetById;
using MeetingReservation.Application.UseCases.Reservation.Register;
using MeetingReservation.Application.UseCases.Reservation.Update;
using MeetingReservation.Application.UseCases.Token;
using MeetingReservation.Application.UseCases.User.ChangePassword;
using MeetingReservation.Application.UseCases.User.Delete;
using MeetingReservation.Application.UseCases.User.GetById;
using MeetingReservation.Application.UseCases.User.Profile;
using MeetingReservation.Application.UseCases.User.Register;
using MeetingReservation.Application.UseCases.User.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingReservation.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetProfileByIdUseCase, GetProfileByIdUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();

        services.AddScoped<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();

        services.AddScoped<IRegisterReservationUseCase, RegisterReservationUseCase>(); 
        services.AddScoped<IGetReservationByIdUseCase, GetReservationByIdUseCase>(); 
        services.AddScoped<IDeleteReservationUseCase, DeleteReservationUseCase>();
        services.AddScoped<IUpdateReservationUseCase, UpdateReservationUseCase>();
	}
}
