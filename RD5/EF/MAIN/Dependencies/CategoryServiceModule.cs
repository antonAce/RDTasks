using Ninject.Modules;
using EFBLL.Services;
using EFBLL.Interfaces;

namespace RDPresentationLayer.Dependencies
{
    public class CategoryServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<DefaultCategoryService>();
        }
    }
}
