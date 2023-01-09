using Agify.DAL.Abstract;
using Agify.DAL.Concrete;
using Agify.DAL.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Agify.DAL
{
    public static class ServiceRegistration
    {
        public static void AddDALService(this IServiceCollection services)
        {
            services.AddDbContext<AgifyDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
            });
        }
    }
}
