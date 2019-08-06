using Ninject.Modules;
using ADOBLL.Services;
using ADOBLL.Interfaces;

namespace RDTask5.Dependencies
{
    public class ProductServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
        }
    }
}
