using Sitecore.Mvc.Controllers;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.Infrastructure;
using Ucommerce.Masterclass.Models;


namespace Ucommerce.Masterclass.Models
{
    public class MasterClassMiniBasketController : SitecoreController
    {
        public ITransactionLibrary TransactionLibrary => ObjectFactory.Instance.Resolve<ITransactionLibrary>();

        public ActionResult Render()
        {
            var miniBasketViewModel = new MiniBasketViewModel();

            if (!TransactionLibrary.HasBasket())
            {
                miniBasketViewModel.Empty = true;
            
                return View("/views/MasterClassMinibasket/index.cshtml", miniBasketViewModel);
            }
            
            var basket = TransactionLibrary.GetBasket(false);
            
            miniBasketViewModel.Empty = false;
            miniBasketViewModel.OrderTotal = new Money(basket.OrderTotal.GetValueOrDefault(0), basket.BillingCurrency.ISOCode).ToString();
            miniBasketViewModel.ItemsInCart = basket.OrderLines.Sum(x => x.Quantity);
            return View("/views/MasterClassMinibasket/index.cshtml", miniBasketViewModel);
        }
    }
}