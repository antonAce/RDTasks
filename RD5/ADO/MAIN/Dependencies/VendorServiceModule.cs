using Ninject.Modules;
using ADOBLL.Services;
using ADOBLL.Interfaces;

namespace RDTask5.Dependencies
{
    public class VendorServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVendorService>().To<VendorService>();
        }
    }
}
