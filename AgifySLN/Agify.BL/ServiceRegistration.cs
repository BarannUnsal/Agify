using Agify.BL.Abstract;
using Agify.BL.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Agify.BL
{
    public static class ServiceRegistration
    {
        public static void AddBLSerivce(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
        }
    }
}
