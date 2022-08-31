using System;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Order
    {
        private static Order instance = null;
        public static Guid Id { get; set; }

        public Account Account { get; set; }

        public List<Item> Items { get; set; }

        public decimal Total { get; set; }

        public static Order GetInstance()
        {
            Id = new Guid();
            if(instance == null)
                instance = new Order();
            return instance;
        }
    }
}
