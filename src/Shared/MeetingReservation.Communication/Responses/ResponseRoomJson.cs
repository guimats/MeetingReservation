using MeetingReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservation.Communication.Responses
{
	public class ResponseRoomJson
	{
		public long RoomId { get; set; }
		public string Name { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public string Location { get; set; } = string.Empty;
		public long UserId { get; set; }
		public IList<ResponseLongReservationJson> Reservations { get; set; } = [];
	}
}
