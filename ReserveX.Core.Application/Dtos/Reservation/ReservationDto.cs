
namespace ReserveX.Core.Application.Dtos.Reservation
{
    public class ReservationDto
    {
        public required string Status { get;  set; }
        public required string CreatedAt { get;  set; }
        public required string Date { get;  set; }
        public required string StartTime { get;  set; }
        public required string EndTime { get;  set; }
        public required string StationName { get; set; }
    }
}
