using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReserveX.Core.Domain.Interfaces;
using ReserveX.Core.Domain.Settings;
using ReserveX.Infraestructure.Persistence.Repositories;

namespace ReserveX.Infraestructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceLayer (this IServiceCollection services , IConfiguration config)
        {
            #region Configurations
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<Contexts.AppDbContext>(opt =>
                {
                    opt.UseInMemoryDatabase("ReserveXDatabase");
                });
            }
            else
            {
                services.AddDbContext<Contexts.AppDbContext>(opt =>
                {
                    var connectionString = config.GetConnectionString("DefaultConnection");
                    opt.UseSqlServer(connectionString);

                }, ServiceLifetime.Transient);
            }
        

            #endregion

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IReservationRepository,ReservationRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IStationRepository, StationRepository>();
            services.AddScoped<IStationResourceRepository, StationResourceRepository>();
            services.AddScoped<IStationScheduleDateRepository, StationScheduleDateRepository>();
            services.AddScoped<IStationScheduleRepository, StationScheduleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            #endregion
        }
    }
}
