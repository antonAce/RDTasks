using Ninject.Modules;
using EFBLL.Services;
using EFBLL.Interfaces;

namespace RDPresentationLayer.Dependencies
{
    public class ProductServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<DefaultProductService>();
        }
    }
}
