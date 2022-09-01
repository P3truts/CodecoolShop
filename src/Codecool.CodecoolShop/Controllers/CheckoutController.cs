using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Collections.Generic;
using Account = Codecool.CodecoolShop.Models.Account;

namespace Codecool.CodecoolShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> logger;

        public IActionResult Index()
        {
            return View();
        }

        public CheckoutController(ILogger<CheckoutController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Account.GetInstance().FirstName = collection["FName"];
                Account.GetInstance().LastName = collection["LName"];
                Account.GetInstance().PhoneNumber = collection["Phone"];
                Account.GetInstance().Email = collection["Email"];
                Account.GetInstance().ShippingAddress = collection["City"] + collection["Country"] + collection["ZipCode"] + collection["Address"];
                Account.GetInstance().BillingAddress = Account.GetInstance().ShippingAddress;
                
                Order.GetInstance().Account = Account.GetInstance();
                Order.GetInstance().Items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                ViewBag.total = Order.GetInstance().Total;
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            var optionC = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = Account.GetInstance().LastName,
                Phone = Account.GetInstance().PhoneNumber
            };

            var serviceC = new CustomerService();

            Customer customer = serviceC.Create(optionC);

            var optionCharge = new ChargeCreateOptions
            {
                Amount = (long)Order.GetInstance().Total,
                Currency = "USD",
                Description = "test",
                Source = stripeToken,
                ReceiptEmail = stripeEmail

            };

            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionCharge);

            if(charge.Status == "succeeded")
            {
                ViewBag.Amount = charge.Amount;
                ViewBag.Customer = customer.Name;
            }

            return View();

        }
    }
}
