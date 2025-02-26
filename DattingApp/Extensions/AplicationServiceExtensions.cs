using DattingAppApi.Data;
using DattingAppApi.Data.Repository;
using DattingAppApi.Helpers;
using DattingAppApi.Interfaces;
using DattingAppApi.Interfaces.Repository;
using DattingAppApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DattingAppApi.Extensions
{
    public static class AplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IPhotoService,PhotoService>();
            services.AddScoped<ILikesRepository,LikesRepository>();
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettinngs>(config.GetSection("CloudinarySettings"));

            return services;
        }
    }
}
