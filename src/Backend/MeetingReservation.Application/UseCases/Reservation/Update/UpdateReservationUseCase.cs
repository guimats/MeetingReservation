using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories;
using MeetingReservation.Domain.Repositories.Reservation;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;
using MeetingReservation.Application.Extensions.Mapping;

namespace MeetingReservation.Application.UseCases.Reservation.Update;

public class UpdateReservationUseCase : IUpdateReservationUseCase
{
	private readonly IReservationUpdateOnlyRepository _updateRepository;
	private readonly IReservationWriteOnlyRepository _writeRepository;
	private readonly ILoggedUser _loggedUser;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateReservationUseCase(
		IReservationUpdateOnlyRepository updateRepository,
		IReservationWriteOnlyRepository writeRepository,
		ILoggedUser loggedUser,
		IUnitOfWork unitOfWork
		)
	{
		_updateRepository = updateRepository;
		_writeRepository = writeRepository;
		_loggedUser = loggedUser;
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseShortReservationJson> Execute(RequestReservationJson request, long reservationID)
	{
		await Validate(request);

		var user = await _loggedUser.User();

		var reservation = await _updateRepository.GetById(reservationID, user.Id);

		if (reservation is null)
			throw new NotFoundException(ResourceMessagesException.RESERVATION_NOT_FOUND);

		reservation = request.MapToReservation(reservation);

		_updateRepository.Update(reservation);

		await _unitOfWork.Commit();

		var response = reservation.MapToShortResponse();

        return response;
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
