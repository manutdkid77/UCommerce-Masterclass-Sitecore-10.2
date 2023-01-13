using Sitecore.Mvc.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.Infrastructure;
using Ucommerce.Search.Models;
using Ucommerce.Search.Slugs;


namespace Ucommerce.Masterclass.Models
{
    public class MasterClassCategoryNavigationController : SitecoreController
    {

        ICatalogLibrary CatalogLibrary => ObjectFactory.Instance.Resolve<ICatalogLibrary>();
        IUrlService UrlService => ObjectFactory.Instance.Resolve<IUrlService>();
        ICatalogContext CatalogContext => ObjectFactory.Instance.Resolve<ICatalogContext>();

        public ActionResult CategoryNavigation()
        {
            var model = new CategoryNavigationViewModel();

            var categories = (CatalogLibrary.GetRootCategories()).Results;

            model.Categories = MapCategories(categories);

            return View("/Views/MasterClassCategoryNavigation/Index.cshtml", model);
        }

        private IList<CategoryViewModel> MapCategories(IList<Category> categories)
        {
            var models = categories
                .Select(category => new CategoryViewModel()
                {
                    Guid = category.Guid,
                    Name = category.Name,
                    Url = UrlService.GetUrl(CatalogContext.CurrentCatalog, new List<Category>() { category })
                })
                .ToList();

            return models;
        }
    }
}