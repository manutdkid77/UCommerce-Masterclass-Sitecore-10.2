using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.Infrastructure;
using Ucommerce.Masterclass.Models;
using Ucommerce.Search;
using Ucommerce.Search.Models;

namespace Ucommerce.Masterclass.Controllers
{
    public class MasterClassProductController : SitecoreController
    {
        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var productModel = new ProductViewModel();

            return View(productModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Index(string sku, string variantSku, int quantity)
        {
            return Index();
        }
        
        private IList<ProductViewModel> MapVariants(ResultSet<Product> variants)
        {
           return new List<ProductViewModel>();
        }
    }
}