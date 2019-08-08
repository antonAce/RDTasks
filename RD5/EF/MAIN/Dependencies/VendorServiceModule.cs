using Ninject.Modules;
using EFBLL.Services;
using EFBLL.Interfaces;

namespace RDPresentationLayer.Dependencies
{
    public class VendorServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVendorService>().To<DefaultVendorService>();
        }
    }
}
