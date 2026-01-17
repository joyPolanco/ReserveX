using Asp.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {


        public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                var jwtConfig = config.GetSection("JwtSettings");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = jwtConfig["Audience"],
                    ValidIssuer = jwtConfig["Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtConfig["Key"] ?? ""))

                };

            });
        }
        /*
        public static void AddAuthorizationExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthorization(options =>
            {

                options.AddPolicy("ActiveUser", policy =>
                {
                policy.Requirements.Add(new ActiveUserRequirement()));
            })
            });
        }
        */
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                List<string> xmlCommentsFiles =
                Directory.GetFiles(
                    AppContext.BaseDirectory,
                    "*.xml",
                    searchOption: SearchOption.TopDirectoryOnly).ToList();

                //  Adding comments to the OpenApi Document
                xmlCommentsFiles.ForEach(x => options.IncludeXmlComments(x));


                //Configuing document versions
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "ReserveX API",
                    Description = "This Api will be responsible for overall data distribution",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "concepcionjohaly@gmail.com",
                        Name = "Johaly Concepción Polanco"
                    }
                });

                options.DescribeAllParametersInCamelCase();
                options.EnableAnnotations();
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Description = "Input your bearer token in this format 'Bearer {your token here}'",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                                Reference= new OpenApiReference
                                {
                                    Type= ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                },
                                Scheme="Bearer",
                                Name="Bearer",
                                In=ParameterLocation.Header
                         } , new List<string>()
                    }

                });


            }).AddSwaggerGenNewtonsoftSupport();

        }


        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
             .AddApiExplorer(opt =>
             {
                 opt.GroupNameFormat = "'v'VVV";
                 opt.SubstituteApiVersionInUrl = true;

             });

        }
    }
}
