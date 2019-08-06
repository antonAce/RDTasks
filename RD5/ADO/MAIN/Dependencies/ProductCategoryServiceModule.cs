using Ninject.Modules;
using ADOBLL.Services;
using ADOBLL.Interfaces;

namespace RDTask5.Dependencies
{
    public class ProductCategoryServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductCategoryService>().To<ProductCategoryService>();
        }
    }
}
