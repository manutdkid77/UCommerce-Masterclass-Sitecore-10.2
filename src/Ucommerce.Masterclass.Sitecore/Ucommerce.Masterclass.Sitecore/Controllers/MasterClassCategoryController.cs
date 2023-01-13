using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.Infrastructure;
using Ucommerce.Search.Extensions;
using Ucommerce.Search.Facets;
using Ucommerce.Search.Models;
using Ucommerce.Search.Slugs;


namespace Ucommerce.Masterclass.Models
{
    public static class FacetedQueryStringExtensions
    {
        public static IList<Facet> ToFacets(this NameValueCollection target)
        {
            var parameters = new Dictionary<string, string>();
            foreach (var queryString in HttpContext.Current.Request.QueryString.AllKeys)
            {
                parameters[queryString] = HttpContext.Current.Request.QueryString[queryString];
            }

            var facetsForQuerying = new List<Facet>();

            foreach (var parameter in parameters.Where(x => !(new[] { "product", "variant", "category", "categories", "catalog" }.Contains(x.Key))))
            {
                var facet = new Facet { FacetValues = new List<FacetValue>(), Name = parameter.Key };
                foreach (var value in parameter.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    facet.FacetValues.Add(new FacetValue() { Value = value });
                }

                facetsForQuerying.Add(facet);
            }

            return facetsForQuerying;
        }
    }

    public class MasterClassCategoryController : SitecoreController
    {
        ICatalogLibrary CatalogLibrary => ObjectFactory.Instance.Resolve<ICatalogLibrary>();
        IUrlService UrlService => ObjectFactory.Instance.Resolve<IUrlService>();
        ICatalogContext CatalogContext => ObjectFactory.Instance.Resolve<ICatalogContext>();

        public MasterClassCategoryController()
        {

        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Index(string sku)
        {
            return Index();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var categoryModel = new CategoryViewModel();

            var currentCategory = CatalogContext.CurrentCategory;

            var facetDictionary = System.Web.HttpContext.Current.Request.QueryString.ToFacets().ToFacetDictionary();

            var facetResultSet = CatalogLibrary.GetProducts(currentCategory.Guid, facetDictionary, 0, 300);

            categoryModel.Products = MapProducts(facetResultSet.Results);

            categoryModel.Facets = MapFacets(facetResultSet.Facets);

            return View(categoryModel);
        }

        private IList<FacetsViewModel> MapFacets(IList<Facet> facets)
        {
            var facetsToReturn = new List<FacetsViewModel>();

            foreach (var f in facets)
            {
                var facetValues = new List<FacetValueViewModel>();

                //calculate facet values
                foreach (var fv in f.FacetValues)
                {
                    facetValues.Add(new FacetValueViewModel()
                    {
                        Key = fv.Value,
                        Count = fv.Count
                    });
                }

                facetsToReturn.Add(new FacetsViewModel()
                {
                    Key = f.Name,
                    DisplayName = f.DisplayName,
                    FacetValues = facetValues
                });
            }

            return facetsToReturn;
        }

        private IList<ProductViewModel> MapProducts(IList<Product> products)
        {
            var prices = CatalogLibrary.CalculatePrices(products.Select(p => p.Guid).ToList(), null);

            var productsToReturn = new List<ProductViewModel>();

            foreach (var p in products)
            {
                var product = new ProductViewModel()
                {
                    Name = p.Name,

                    LongDescription = p.LongDescription,

                    Prices = prices.Items.Where(x => x.ProductGuid == p.Guid && x.PriceGroupGuid == CatalogContext.CurrentPriceGroup.Guid).ToList(),

                    Url = UrlService.GetUrl(CatalogContext.CurrentCatalog, new List<Category>() { CatalogContext.CurrentCategory }, p),

                    Sku = p.Sku,

                    Sellable = p.ProductType == ProductType.Product || p.ProductType != ProductType.ProductFamily,

                    PrimaryImageUrl = p.PrimaryImageUrl,
                };

                productsToReturn.Add(product);
            }

            return productsToReturn;
        }
    }
}