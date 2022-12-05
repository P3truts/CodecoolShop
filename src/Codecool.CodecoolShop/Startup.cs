using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Product = Codecool.CodecoolShop.Models.Product;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddSession();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey ="sk_test_51LcqfIF3ujix2guhFghtYRFKAuQCWR59YbXbJKIoO1Ftjiuth3b8riErOdRHtKnPuacuHUZBqxY4EL1VOOP5DnhA00WQQ60moC";
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();

            Supplier amazon = new Supplier{Name = "Amazon", Description = "Digital content and services"};
            supplierDataStore.Add(amazon);
            Supplier lenovo = new Supplier{Name = "Lenovo", Description = "Computers"};
            supplierDataStore.Add(lenovo);
            ProductCategory tablet = new ProductCategory {Name = "Tablets", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDataStore.Add(tablet);
            productDataStore.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "EUR", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "EUR", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            productDataStore.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "EUR", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });

            Supplier sektor = new Supplier { Name = "Sektor", Description = "Interior Design" };
            supplierDataStore.Add(sektor);
            ProductCategory chair = new ProductCategory { Name = "Chairs", Department = "Furniture", Description = "A classic furniture object to sit on." };
            productCategoryDataStore.Add(chair);
            Supplier aliExpress = new Supplier { Name = "Ali Express", Description = "Online Market" };
            supplierDataStore.Add(aliExpress);
            productDataStore.Add(new Product { Name = "Amazon Deluxe Office Leather", DefaultPrice = 288.85m, Currency = "EUR", Description = "Tall and Big Ergonomic Office High Back Chair Boss Work Task Computer Executive Comfort Comfortable Loop Arms.", ProductCategory = chair, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Ali Express Scorpion Gaming Chair", DefaultPrice = 3598.9m, Currency = "EUR", Description = "Shop scorpion game chair with fast delivery and free shipping. The seat is covered in synthetic leather.", ProductCategory = chair, Supplier = aliExpress });
            productDataStore.Add(new Product { Name = "Sektor Hush Accoustic Pod", DefaultPrice = 399.9m, Currency = "EUR", Description = "The best place to have a meeting without distractions from your colleagues or viceversa.", ProductCategory = chair, Supplier = sektor });

            Supplier anchor = new Supplier { Name = "Anchor", Description = "Sports Equipment" };
            supplierDataStore.Add(anchor);
            ProductCategory weights = new ProductCategory { Name = "Weights", Department = "Sports", Description = "Typical weights for workout." };
            productCategoryDataStore.Add(weights);

            productDataStore.Add(new Product { Name = "Anchor 20kg Dumbbells Set", DefaultPrice = 36.99m, Currency = "EUR", Description = "This 20kg Spinlock Dumbbell Set is made from durable Vinyl material offering you variety of weight selection.", ProductCategory = weights, Supplier = anchor });

            ProductCategory sportBalls = new ProductCategory { Name = "Sport Balls", Department = "Sports", Description = "Round balls for different ball games." };
            productCategoryDataStore.Add(sportBalls);

            Supplier nike = new Supplier { Name = "Nike", Description = "Sports Equipment" };
            supplierDataStore.Add(nike);

            productDataStore.Add(new Product { Name = "Nike Football Size 5", DefaultPrice = 14.99m, Currency = "EUR", Description = "Football\r\nWater Resistant\r\nOuter Material: PUPVC\r\nCore Material: PVC 4 PLY\r\nWeight: 350-450 g", ProductCategory = sportBalls, Supplier = nike });

            ProductCategory surfboards = new ProductCategory { Name = "Surfboards", Department = "Sports", Description = "Curved, slim and slick boards to ride the waves." };
            productCategoryDataStore.Add(surfboards);

            Supplier decathlon = new Supplier { Name = "Decathlon", Description = "Sports Equipment Shop" };
            supplierDataStore.Add(decathlon);

            productDataStore.Add(new Product { Name = "Decathlon Surfboard Leash 3 Fins", DefaultPrice = 199.99m, Currency = "EUR", Description = "The perfect board for small waves and constant practicing. A board that's perfect for small and heavier surfers.", ProductCategory = surfboards, Supplier = decathlon });
        }
    }
}
