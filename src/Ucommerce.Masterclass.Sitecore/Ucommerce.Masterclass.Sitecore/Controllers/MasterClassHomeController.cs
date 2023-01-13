using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Ucommerce.Masterclass.Models
{
    public class MasterClassHomeController : SitecoreController
    { 
        public override ActionResult Index()
        {
            return View("/Views/MasterClassHome/Index.cshtml");
        }
    }
}