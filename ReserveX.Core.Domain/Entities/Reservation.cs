using ReserveX.Core.Domain.Common.enums;


namespace ReserveX.Core.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; private set; }
        public Guid SlotId { get; private set; }
        public Guid UserId { get; private set; }

        public ReservationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public Slot ?Slot { get; private set; }
        public User? User { get; private set; }
    }
}
