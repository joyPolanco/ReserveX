using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Domain.Entities
{
    public class Slot
    {
        public Guid Id { get; private set; }
        public Guid StationId { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public int TotalCapacity { get; private set; }
        public int AvailableCapacity { get; private set; }

        public bool IsActive { get; private set; }

        public byte[] ?RowVersion { get; private set; }

        public Station? Station { get; private set; }
        public ICollection<Reservation>? Reservations { get; private set; }

    }
}
