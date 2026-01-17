using ReserveX.Core.Domain.Common.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Domain.Entities
{
    public class StationSchedule
    {
        public Guid Id { get; set; }

        public Guid StationId { get; set; }
        public Station Station { get; set; } = null!;

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Status Status { get; set; }

        public ICollection<StationScheduleDate> Dates { get; set; } = new List<StationScheduleDate>();
    }

}
