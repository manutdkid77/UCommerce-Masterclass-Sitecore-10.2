using Sitecore.Mvc.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.Infrastructure;
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

        public ActionResult MasterClassProductSearchResult()
        {
            var model = new ProductListViewModel();

            var searchTerm = GetSearchTerm();

            //resolve and access the search index for Products
            var index = ObjectFactory.Instance.Resolve<IIndex<Ucommerce.Search.Models.Product>>();

            var guid = System.Guid.Parse("92CDC7C2-B511-41C6-ADF5-37E66CD52366");

            //this is lower level code implementation of CatalogLibrary.GetProducts()
            //https://docs.ucommerce.net/ucommerce/v9.6.1/search-and-indexing/Search-api.html
            var results = index.Find<Ucommerce.Search.Models.Product>()
                .Where(p =>
                           p.DisplayName == Match.Wildcard($"*{searchTerm}*") //similar to SQL LIKE operator: %searchterm%
                        || p.DisplayName == Match.FullText(searchTerm)
                        || p.ShortDescription == Match.FullText(searchTerm)
                        || p.Name == Match.Fuzzy(searchTerm, 2)
                        || p.Sku == Match.Literal(searchTerm)
                    ).ToList().Results;

            //we can also combine the above query with the facets selected by user (overheard involved)

            //var result = index.Find<Ucommerce.Search.Models.Product>()
            //    .Where(
            //        product => product.VariantSku == null
            //        && product.ParentProduct == null
            //        && product.ProductDefinition == Constants.Guid 
            //        &&
            //        (product.Sku.Contains(searchTerm) //Sku doesnt support FullText so use .Contains()
            //            || product.DisplayName == Match.FullText(searchTerm)
            //            || product.ShortDescription == Match.FullText(searchTerm)
            //            || product.LongDescription == Match.FullText(searchTerm)
            //        )).ToList();

            model.ProductViewModels = MapProducts(results);
            return View("/Views/MasterClassProductSearchResult/Index.cshtml", model);

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