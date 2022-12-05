using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class Item
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
