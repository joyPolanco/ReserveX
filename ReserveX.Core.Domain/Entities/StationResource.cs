

namespace ReserveX.Core.Domain.Entities
{
    public class StationResource
    {
        public Guid Id { get; set; }

        public Guid StationId { get; set; }
        public Station Station { get; set; } = null!;

        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;
    }

}
