using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReserveX.Core.Domain.Entities;


namespace ReserveX.Infraestructure.Persistence.EntitiesConfiguration
{
    public class ReservationEntityConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
           builder.HasKey(x=>x.Id);
           builder.Property(x=>x.Status).IsRequired();
          
           
        }

    }
}
