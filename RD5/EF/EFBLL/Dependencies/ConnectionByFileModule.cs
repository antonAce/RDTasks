using Ninject.Modules;
using EFDAL.Interfaces;
using EFDAL.Repositories;

namespace EFBLL.Dependencies
{
    public class ConnectionByFileModule : NinjectModule
    {
        private string _fileName, _connectionName;

        public ConnectionByFileModule(string fileName, string connectionName) { _fileName = fileName; _connectionName = connectionName; }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWorkConnectionByFile>()
                .WithConstructorArgument("fileName", _fileName)
                .WithConstructorArgument("connectionName", _connectionName);
        }
    }
}
