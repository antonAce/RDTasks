using Ninject.Modules;
using ADODAL.Interfaces;
using ADODAL.Repositories;

namespace ADOBLL.Dependencies
{
    public class ServiceModule : NinjectModule
    {
        private string _connectionName;

        public ServiceModule(string connectionName) { _connectionName = connectionName; }

        public override void Load() { Bind<IUnitOfWork>().To<ADOUnitOfWork>().WithConstructorArgument(_connectionName); }
    }
}
