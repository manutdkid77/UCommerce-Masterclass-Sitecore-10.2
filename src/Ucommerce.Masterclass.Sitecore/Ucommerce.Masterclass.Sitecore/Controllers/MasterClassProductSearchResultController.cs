using Sitecore.Mvc.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.Infrastructure;
using Ucommerce.Masterclass.Models;
using Ucommerce.Search;
using Ucommerce.Search.Models;
using Ucommerce.Search.Slugs;


namespace Ucommerce.Masterclass.Models
{
    public class MasterClassProductSearchResultController : SitecoreController
    {
        public ICatalogLibrary CatalogLibrary => ObjectFactory.Instance.Resolve<ICatalogLibrary>();
        public IUrlService UrlService => ObjectFactory.Instance.Resolve<IUrlService>();

        public ICatalogContext CatalogContext => ObjectFactory.Instance.Resolve<ICatalogContext>();

        private string GetSearchTerm()
        {
            return Request.QueryString["Query"];
        }
        
        public MasterClassProductSearchResultController()
        {
            
        }

        public static class Constants
        {
            public static System.Guid Guid = System.Guid.Parse("92CDC7C2-B511-41C6-ADF5-37E66CD52366");
        }

        public ActionResult Index()
        {
            var model = new ProductListViewModel();

            var searchTerm = GetSearchTerm();

            var index = ObjectFactory.Instance.Resolve<IIndex<Ucommerce.Search.Models.Product>>();

            var guid = System.Guid.Parse("92CDC7C2-B511-41C6-ADF5-37E66CD52366");

            var result = index.Find()
                .Where(
                    product => product.VariantSku == null
                    && product.ParentProduct == null
                    && product.ProductDefinition == Constants.Guid 
                    &&
                    (product.Sku.Contains(searchTerm)
                        || product.DisplayName == Match.FullText(searchTerm)
                        || product.ShortDescription == Match.FullText(searchTerm)
                        || product.LongDescription == Match.FullText(searchTerm)
                    )).ToList();

            model.ProductViewModels = MapProducts(result.Results);
            return View(model);
            
        }
        
        private IList<ProductViewModel> MapProducts(IList<Product> products)
        {
            var prices = CatalogLibrary.CalculatePrices(products.Select(x => x.Guid).ToList());
            
            return products.Select(product => new ProductViewModel()
            {
                LongDescription = product.LongDescription,
                IsVariant = product.ProductType == ProductType.Variant,
                Sellable = product.ProductType == ProductType.Product || product.ProductType == ProductType.Variant,
                PrimaryImageUrl = product.PrimaryImageUrl,
                Sku = product.Sku,
                Name = product.DisplayName,
                Prices = prices.Items.Where(price => price.ProductGuid == product.Guid && price.PriceGroupGuid == CatalogContext.CurrentPriceGroup.Guid).ToList(),
                ShortDescription = product.ShortDescription,
                Url = UrlService.GetUrl(CatalogContext.CurrentCatalog, product)
            }).ToList();
        }
    }
}