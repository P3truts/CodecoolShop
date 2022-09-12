using Codecool.CodecoolShop.Models;
using NSubstitute;
using System.Drawing;

namespace Codecool.CodecoolShop.UnitTests
{
    public class Tests
    {
        private IProductDao productDao;
        private IProductCategoryDao productCategoryDao;
        private ISupplierDao supplierDao;

        [SetUp]
        public void Setup()
        {
            productDao = ProductDaoMemory.GetInstance();
            productCategoryDao = ProductCategoryDaoMemory.GetInstance();
            supplierDao = SupplierDaoMemory.GetInstance();

            Supplier amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
            supplierDao.Add(amazon);
            Supplier lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
            supplierDao.Add(lenovo);

            ProductCategory tablet = new ProductCategory { Name = "Tablets", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDao.Add(tablet);

            productDao.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "EUR", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            productDao.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "EUR", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            productDao.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "EUR", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });
        }

        [Test]
        public void GetAllProductsCountIsEqual()
        {
            var expectedResult = 3;

            Assert.That(expectedResult, Is.EqualTo(productDao.GetAll().Count()));
        }
    }
}