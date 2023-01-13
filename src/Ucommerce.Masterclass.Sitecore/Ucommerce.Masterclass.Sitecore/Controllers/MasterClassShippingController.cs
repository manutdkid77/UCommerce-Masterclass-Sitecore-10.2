using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Ucommerce.Masterclass.Models
{
    public class MasterClassShippingController : SitecoreController
    {
        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var shippingViewModel = new ShippingViewModel();
            
            return View(shippingViewModel);
        }


        [HttpPost]
        public ActionResult Index(int SelectedShippingMethodId)
        {
            return Redirect("/payment");
        }
    }
}