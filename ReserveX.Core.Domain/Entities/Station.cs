

using ReserveX.Core.Domain.Common.enums;

namespace ReserveX.Core.Domain.Entities
{
    public class Station
    {
        public Guid Id { get; private  set; }= Guid.NewGuid();
        public required string Name { get;  set; }
        public string Description { get;  set; } = string.Empty;
        public int SlotDurationMinutes { get;  set; }
        public int SlotsCapacity { get;  set; }
        
        public Status Status { get; set; }
        public DateTime CreatedAt { get;  set; }= DateTime.UtcNow;

        public ICollection<StationSchedule> ?Schedules { get;  set; }
        public ICollection<Slot>? Slots { get;  set; }
        public ICollection<StationResource> StationResources { get; set; } = new List<StationResource>();

    }
}
