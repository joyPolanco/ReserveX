using Microsoft.EntityFrameworkCore;
using ReserveX.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Infraestructure.Persistence.Seeds
{
    public static class UserSeed
    {


        public static void CreateSeeds(this   ModelBuilder  modelBuilder)
        {

            //Pa$swOrd1

            modelBuilder.Entity<User>().HasData(
                new User
                
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),

                    Email = "johalypolanco13@gmail.com",
                    LastName="Concepcion",
                    Name="Johaly",
                    PasswordHash= "$2b$12$p5ZtMFQRYB7y0wi/BGOOP.VxMAQP70mh4kVInaGTGj4R2ce0XZZHm",
                    CreatedAt = new DateTime(2024, 1, 1),
                    Role = Core.Domain.Common.enums.UserRole.ADMIN,
                    Status= Core.Domain.Common.enums.Status.ACTIVE,
                   
                }
                );
        }
    }
}
