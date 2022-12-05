using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using System.Runtime.Intrinsics.X86;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
        }

        public IActionResult Index(int supplier = 0, int category = 0)
        {
            if(supplier == 0 && category == 0)
            {
                var products = ProductService.GetAllProducts();
            return View(products.ToList());
        }
            else if(supplier != 0 && category == 0)
            {
                var products = ProductService.GetProductsBySupplier(supplier);
                return View(products.ToList());
            }
            else if(supplier == 0 && category != 0)
            {
                var products = ProductService.GetProductsForCategory(category);
                return View(products.ToList());
            }
            return View();
        }

        [HttpGet]
        [Route("Index/Products")]
        public PartialViewResult Products(int supplier = 0, int category = 0)
        {
            if (supplier == 0 && category == 0)
            {
                var products = ProductService.GetAllProducts();
                return PartialView(products.ToList());
            }
            else if (supplier != 0 && category == 0)
            {
                var products = ProductService.GetProductsBySupplier(supplier);
                return PartialView(products.ToList());
            }
            else if (supplier == 0 && category != 0)
            {
                var products = ProductService.GetProductsForCategory(category);
                return PartialView(products.ToList());
            }
            return PartialView();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
