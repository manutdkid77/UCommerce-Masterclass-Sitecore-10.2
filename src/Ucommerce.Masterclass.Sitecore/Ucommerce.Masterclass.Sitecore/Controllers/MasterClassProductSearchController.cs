using Sitecore.Mvc.Controllers;
using System.Web.Mvc;


namespace Ucommerce.Masterclass.Models
{
    public class MasterClassProductSearchController : SitecoreController
    {
        public ActionResult ProductSearch()
        {
            return View("/views/MasterClassProductSearch/index.cshtml");
        }
    }
}