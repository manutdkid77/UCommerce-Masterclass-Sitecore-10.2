using Sitecore.Mvc.Controllers;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.EntitiesV2;
using Ucommerce.Infrastructure;
using Ucommerce.Masterclass.Models;


namespace Ucommerce.Masterclass.Models
{
    public class MasterClassCheckoutPreviewController : SitecoreController
    {
        public ITransactionLibrary TransactionLibrary => ObjectFactory.Instance.Resolve<ITransactionLibrary>();

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        private AddressViewModel MapAddress(OrderAddress address)
        {
            var addressModel = new AddressViewModel();
            
            addressModel.FirstName = address.FirstName;
            addressModel.EmailAddress = address.EmailAddress;
            addressModel.LastName = address.LastName;
            addressModel.PhoneNumber = address.PhoneNumber;
            addressModel.MobilePhoneNumber = address.MobilePhoneNumber;
            addressModel.Line1 = address.Line1;
            addressModel.Line2 = address.Line2;
            addressModel.PostalCode = address.PostalCode;
            addressModel.City = address.City;
            addressModel.State = address.State;
            addressModel.Attention = address.Attention;
            addressModel.CompanyName = address.CompanyName;
            addressModel.CountryName = address.Country.Name;

            return addressModel;
        }


        [HttpPost]
        public ActionResult Index(int complete)
        {
            return Redirect("/complete");
        }
    }
}