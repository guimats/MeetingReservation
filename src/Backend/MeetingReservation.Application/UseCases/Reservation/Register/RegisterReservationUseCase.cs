using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Reservation;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;
using System.Threading.Tasks;

namespace MeetingReservation.Application.UseCases.Reservation.Register;

public class RegisterReservationUseCase : IRegisterReservationUseCase
{
    private readonly IReservationWriteOnlyRepository _writeRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterReservationUseCase(
        IReservationWriteOnlyRepository writeRepository,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseShortReservationJson> Execute(RequestReservationJson request)
    {
        await Validate(request);

        var user = await _loggedUser.User();
        request.UserId = user.Id;

        var reservation = request.MapToReservation();

        await _writeRepository.Add(reservation);

        await _unitOfWork.Commit();

        return new ResponseShortReservationJson
        {
            Id = reservation.Id,
            Name = reservation.Name,
            UserId = reservation.UserId,
        };
    }

    private async Task Validate(RequestReservationJson request)
    {
        var validator = new ReservationValidator();

        var result = validator.Validate(request);

        bool isOcuppied = await _writeRepository.IsTimeOccupied(request.RoomId, request.InitialTime, request.EndTime);

        if (isOcuppied)
		    throw new ErrorOnValidationException([ResourceMessagesException.RESERVATION_OCCUPIED]);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
