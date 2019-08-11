using Microsoft.Extensions.DependencyInjection;

using OrderManagerDAL.Interfaces;
using OrderManagerDAL.Repositories;

namespace OrderManagerBLL.Dependencies
{
    public static class ServiceProviderUnitOfWork
    {
        public static void AddUnitOfWorkByFileConfig(this IServiceCollection services, string filename, string connectionname)
        {
            services.AddSingleton<IUnitOfWork>(s => new UOWConfigFileConnection(filename, connectionname));
        }

        public static void AddUnitOfWorkByConnectionString(this IServiceCollection services, string connectionstring)
        {
            services.AddSingleton<IUnitOfWork>(s => new UOWStringConnection(connectionstring));
        }
    }
}
