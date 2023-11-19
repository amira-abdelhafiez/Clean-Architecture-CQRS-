using Microsoft.Extensions.DependencyInjection;
using PioneersAPI.Application.Interfaces.Repositories;
using PioneersAPI.Application.Interfaces;
using PioneersAPI.Infrastructure.Persisance.Repositories;
using PioneersAPI.Infrastructure.Shared;
using PioneersAPI.Infrastructure.Persisance.DBContext;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PioneersAPI.Infrastructure
{
    public static class ServiceRegisteration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IGoodsRepositoryAsync, GoodsRepositoryAsync>();
            #endregion
        }

        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
