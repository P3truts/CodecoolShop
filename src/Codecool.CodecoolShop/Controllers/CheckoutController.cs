using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

                ViewBag.totalTitle = Order.GetInstance().Total;
                ViewBag.totalAmount = Order.GetInstance().Total * 100;
                return View(Order.GetInstance().Items);
            }
            catch
            {
                return View();
            }
        }
    }
}
