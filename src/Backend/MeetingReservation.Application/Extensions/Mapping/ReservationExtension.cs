using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Application.Extensions.Mapping;

public static class ReservationExtension
{
    public static Reservation MapToReservation(this RequestReservationJson request)
    {
        return new Reservation
        {
            Name = request.Name,
            Description = request.Description,
            InitialTime = request.InitialTime,
            EndTime = request.EndTime,
            Participants = request.Participants,
            UserId = request.UserId
        };
    }

    public static Reservation MapToReservation(this RequestReservationJson request, Reservation reservation)
    {
        reservation.Name = request.Name;
        reservation.Description = request.Description;
        reservation.InitialTime = request.InitialTime;
        reservation.EndTime = request.EndTime;
        reservation.Participants = request.Participants;

        return reservation;
    }

    public static ResponseLongReservationJson MapToLongReservation(this Reservation reservation)
    {
        return new ResponseLongReservationJson
        {
            Id = reservation.Id,
            Name = reservation.Name,
            Description = reservation.Description,
            InitialTime = reservation.InitialTime,
            EndTime = reservation.EndTime,
            Participants = reservation.Participants,
            UserId = reservation.UserId
        };
    }
}
