using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReserveX.Core.Domain.Entities;


namespace ReserveX.Infraestructure.Persistence.EntitiesConfiguration
{
    public class ResourceEntityConfiguuration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Type).IsRequired();
            builder.Property(x=>x.Status).IsRequired();

            builder.HasMany(x => x.StationResources)
                .WithOne(x => x.Resource)
                .HasForeignKey(x => x.StationId);
        }
       
    }
}
