using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Account = Codecool.CodecoolShop.Models.Account;

namespace Codecool.CodecoolShop.Controllers
{
    public class PayController : Controller
    {
        public IActionResult Index(string stripeToken, string stripeEmail)
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

            if (charge.Status == "succeeded")
            {
                ViewBag.Amount = charge.Amount;
                ViewBag.Customer = customer.Name;
                return View();
            }
            return Redirect("Failed");

        }

        public IActionResult Failed()
        {
            return View();

        }
    }
}
