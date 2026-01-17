using ReserveX.Core.Domain.Common.enums;
using System.Security.AccessControl;


namespace ReserveX.Core.Domain.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public Common.enums.ResourceType Type { get; set; }

        public Status Status { get; set; }

        public ICollection<StationResource> StationResources { get; set; } = new List<StationResource>();
    }


}
