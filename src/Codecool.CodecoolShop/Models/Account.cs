using Codecool.CodecoolShop.Daos.Implementations;

namespace Codecool.CodecoolShop.Models
{
    public class Account
    {
        private static Account instance;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }

        public static Account GetInstance()
        {
            if (instance == null)
            {
                instance = new Account();
            }

            return instance;
        }


        public override string ToString()
        {
            return $"{FirstName}, {LastName}, {Email}, {PhoneNumber}, {ShippingAddress}, {BillingAddress}";
        }
    }
}
