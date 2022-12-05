using Microsoft.AspNetCore.Hosting.Server;
using Stripe;
using System;
using System.Collections.Generic;
using File = System.IO.File;
using System.Web;

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


        public void Serialize()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            // Write the json string to file.
            string filePath = "https://localhost:44368/test.json";
            //Server.MapPath("test.asp");
            if (json != null)
            {
                if (File.Exists(filePath))
                {
                    Console.WriteLine("The file exists.");
                } else
                {
                    File.Create(filePath);
                }
            }
            System.IO.File.WriteAllText(filePath, json);
        }
    }
}
