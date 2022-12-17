using FinTrackApi.Data;
using FinTrackApi.Data.Models;
using FinTrackApi.Data.Seeding;
using FinTrackApi.Infrastructure.Services;
using FinTrackApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using System.Text;

namespace FinTrackApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static  IServiceCollection RegisterServices(this IServiceCollection servicesCollection, IConfiguration configuration)
        {
            servicesCollection.AddControllers();
            servicesCollection.AddEndpointsApiExplorer();
            servicesCollection.AppServices();
            servicesCollection.AddIdentity();
            servicesCollection.AddSwagger();
            servicesCollection.AddDatabase(configuration);
            
            var appSettings = servicesCollection.GetApplicationSettings(configuration);
            servicesCollection.AddJwtTokenAtuhentication(appSettings);

            return servicesCollection;
        }

        public static IServiceCollection AddEndPotins(this IServiceCollection services)
        => services.AddEndpointsApiExplorer();


        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuretion)
       => services.AddDbContext<FinTrackApiDbContext>(options =>
            options.UseSqlServer(configuretion.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AppServices(this IServiceCollection services)
        {

            services.AddTransient<ISeeder, RoleSeeder>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<ITransactionAccountService, TransactionAccountService>();

            return services;

        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                    options.ClaimsIdentity.RoleClaimType.IsNormalized();
                })
            .AddEntityFrameworkStores<FinTrackApiDbContext>();

            return services;
        }

        public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);

            var appSettings = applicationSettingsConfiguration.Get<AppSettings>();

            return appSettings;
        }

        public static IServiceCollection AddJwtTokenAtuhentication(this IServiceCollection services, AppSettings settings)
        {
            var key = Encoding.ASCII.GetBytes(settings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });
            return services;
        }

        public static IServiceCollection  AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {

                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert Jwt token",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                  {
                        new OpenApiSecurityScheme
                        {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                        },
                            new string[] { }
                  }
                });
            });
            return services;
        }
    }
}
