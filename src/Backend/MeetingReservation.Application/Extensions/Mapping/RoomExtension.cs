using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Application.Extensions.Mapping;

public static class RoomExtension
{
	public static Room MapToRoom(this RequestRoomJson request)
	{
		return new Room
		{
			Name = request.Name,
			Capacity = request.Capacity,
			Location = request.Location,
			UserId = request.UserId
		};
	}

	public static Room MapToRoom(this RequestRoomJson request, Room room)
	{
		room.Name = request.Name;
		room.Capacity = request.Capacity;
		room.Location = request.Location;

		return room;
	}

	public static ResponseRoomJson MapToResponse(this Room room)
	{
		var reservations = room.Reservations?
		.Select(reservation => reservation.MapToLongReservation())
		.ToList()
		?? new List<ResponseLongReservationJson>(); // caso reservation seja null

		return new ResponseRoomJson
		{
			RoomId = room.Id,
			Name = room.Name,
			Capacity = room.Capacity,
			Location = room.Location,
			UserId = room.UserId,
			Reservations = reservations
		};
	}

	public static ResponseShortRoomJson MapToShortResponse(this Room room)
	{
		return new ResponseShortRoomJson
		{
			Id = room.Id,
			Name = room.Name,
			Capacity = room.Capacity,
			Location = room.Location
		};
	}

	public static ResponseRoomsJson MapToRooms(this IList<Room> rooms)
	{
		var response = rooms.Select(room => room.MapToShortResponse()).ToList();

		return new ResponseRoomsJson { 
			Rooms = response
		};
	}
}
