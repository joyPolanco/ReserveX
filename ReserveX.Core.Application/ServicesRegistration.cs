// ReserveX.Core.Application/ServicesRegistration.cs
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReserveX.Core.Application.Behaviors;
using ReserveX.Core.Application.Features.Login.Commands.Login;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Core.Application.Services;
using ReserveX.Core.Domain.Settings;

namespace ReserveX.Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            // Configuraciones
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

            // Servicios
            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // **VALIDATORS PRIMERO**
            services.AddValidatorsFromAssembly(
                typeof(LoginCommandValidator).Assembly,
                includeInternalTypes: true
            );

            // **MEDIATR CON BEHAVIORS**
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}