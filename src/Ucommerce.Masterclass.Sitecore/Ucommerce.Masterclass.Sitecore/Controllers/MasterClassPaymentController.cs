using Sitecore.Mvc.Controllers;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.Infrastructure;
using Ucommerce.Masterclass.Models;


namespace Ucommerce.Masterclass.Models
{
    public class MasterClassPaymentController : SitecoreController
    {
        public ITransactionLibrary TransactionLibrary => ObjectFactory.Instance.Resolve<ITransactionLibrary>();

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var paymentViewModel = new PaymentViewModel();

            return View(paymentViewModel);
        }


        [HttpPost]
        public ActionResult Index(int selectedPaymentMethodId)
        {
            return Redirect("/preview");
        }
    }
}