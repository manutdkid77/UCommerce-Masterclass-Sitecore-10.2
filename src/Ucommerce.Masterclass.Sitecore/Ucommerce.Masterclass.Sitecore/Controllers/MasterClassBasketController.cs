using Sitecore.Mvc.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.EntitiesV2;
using Ucommerce.Infrastructure;
using Ucommerce.Marketing;
using Ucommerce.Marketing.Awards.AwardResolvers;
using Ucommerce.Marketing.TargetingContextAggregators;
using Ucommerce.Marketing.Targets.TargetResolvers;
using Ucommerce.Masterclass.Models;

namespace Ucommerce.Masterclass.Models
{
    public class MasterClassBasketController : SitecoreController
    {
        public ITransactionLibrary TransactionLibrary => ObjectFactory.Instance.Resolve<ITransactionLibrary>();

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Index(int quantity, int orderlineId)
        {
            return Index();
        }
    }
}