using Sitecore.Mvc.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;
using Ucommerce.Api;
using Ucommerce.EntitiesV2;
using Ucommerce.Infrastructure;
using Ucommerce.Masterclass.Models;

namespace Ucommerce.Masterclass.Controllers
{
    public class MasterClassOrderConfirmationController : SitecoreController
    {
        public ITransactionLibrary TransactionLibrary => ObjectFactory.Instance.Resolve<ITransactionLibrary>();

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var orderGuidParameterFromQueryString = System.Web.HttpContext.Current.Request.QueryString["OrderGuid"];
            
            var basket = TransactionLibrary.GetPurchaseOrder(Guid.Parse(orderGuidParameterFromQueryString));

            PurchaseOrderViewModel purchaseOrderViewModel = MapPurchaseOrder(basket);

            purchaseOrderViewModel.BillingAddress = MapAddress(basket.BillingAddress);
            purchaseOrderViewModel.ShippingAddress = MapAddress(basket.Shipments.First().ShipmentAddress);
            
            return View(purchaseOrderViewModel);
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
        
        private PurchaseOrderViewModel MapPurchaseOrder(PurchaseOrder basket)
        {
            var model = new PurchaseOrderViewModel();

            model.DiscountTotal =
                new Money(basket.Discount.GetValueOrDefault(), basket.BillingCurrency.ISOCode)
                    .ToString();
            model.SubTotal =
                new Money(basket.SubTotal.GetValueOrDefault(), basket.BillingCurrency.ISOCode)
                    .ToString();
            model.TaxTotal =
                new Money(basket.TaxTotal.GetValueOrDefault(), basket.BillingCurrency.ISOCode)
                    .ToString();
            model.ShippingTotal =
                new Money(basket.ShippingTotal.GetValueOrDefault(), basket.BillingCurrency.ISOCode).ToString();
            model.PaymentTotal =
                new Money(basket.PaymentTotal.GetValueOrDefault(), basket.BillingCurrency.ISOCode).ToString();
            model.OrderTotal =
                new Money(basket.OrderTotal.GetValueOrDefault(), basket.BillingCurrency.ISOCode).ToString();
            
            model.OrderLines = basket.OrderLines.Select(orderLine => new OrderlineViewModel()
            {
                Quantity = orderLine.Quantity,
                ProductName = orderLine.ProductName,
                Total = new Money(orderLine.Total.GetValueOrDefault(), basket.BillingCurrency.ISOCode).ToString(),
                Discount = orderLine.Discount,
                OrderLineId = orderLine.OrderLineId
            }).ToList();

            return model;
        }
    }
}