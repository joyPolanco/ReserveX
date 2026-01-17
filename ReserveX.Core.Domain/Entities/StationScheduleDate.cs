using ReserveX.Core.Domain.Common.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Domain.Entities
{
    public class StationScheduleDate
    {
        public Guid Id { get; set; }

        public Guid StationScheduleId { get; set; }
        public StationSchedule StationSchedule { get; set; } = null!;

        public DateOnly Date { get; set; }
        public Status Status { get; set; }
    }

}
