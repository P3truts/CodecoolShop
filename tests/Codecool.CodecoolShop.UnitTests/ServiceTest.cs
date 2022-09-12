using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using NSubstitute;
using System.Drawing;

namespace Codecool.CodecoolShop.UnitTests
{
    public class Tests
    {
        private IProductDao _productDao;
        private IProductCategoryDao _productCategoryDao;
        private ISupplierDao _supplierDao;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productDao = ProductDaoMemory.GetInstance();
            _productCategoryDao = ProductCategoryDaoMemory.GetInstance();
            _supplierDao = SupplierDaoMemory.GetInstance();

            _productService = new ProductService(_productDao, _productCategoryDao, _supplierDao);


            Supplier amazon = new Supplier { Name = "Amazon", Description = "Digital content and services" };
            _supplierDao.Add(amazon);
            Supplier lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
            _supplierDao.Add(lenovo);

            ProductCategory tablet = new ProductCategory { Name = "Tablets", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            _productCategoryDao.Add(tablet);

            _productDao.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "EUR", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, Supplier = amazon });
            _productDao.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "EUR", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, Supplier = lenovo });
            _productDao.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "EUR", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, Supplier = amazon });
        }

        [Test]
        public void GetAllProductsCountIsEqual()
        {
            var expectedResult = 3;

            Assert.That(expectedResult, Is.EqualTo(_productService.GetAllProducts().Count()));
        }

        [Test]
        public void GetAllProductsEmptyReturnsEmpty()
        {
            var emptyProductDao = Substitute.For<IProductDao>();
            var emptyCategoryDao = Substitute.For<IProductCategoryDao>();
            var emptySupplierDao = Substitute.For<ISupplierDao>();
            var mockProductService = new ProductService(emptyProductDao, emptyCategoryDao, emptySupplierDao);

            List<Product> expectedResult = new List<Product>();

            Assert.That(mockProductService.GetAllProducts(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetProductForCategoryIsCorrectCategory()
        {
            int categoryId = 1;
            var expectedCategory = "tablets";

            Assert.That(_productService.GetProductsForCategory(categoryId).FirstOrDefault().ProductCategory.Name.ToUpper(), Is.EqualTo(expectedCategory.ToUpper()));
        }

        [Test]
        public void GetProductForCategoryCountIsEqual()
        {
            int categoryId = 1;
            var expectedCategoryCount = 3;

            Assert.That(_productService.GetProductsForCategory(categoryId).Count(), Is.EqualTo(expectedCategoryCount));
        }

        [Test]
        public void GetProductBySupplierIsCorrectSupplier()
        {
            int supplierId = 1;
            var expectedSupplier = "amazon";

            Assert.That(_productService.GetProductsBySupplier(supplierId).FirstOrDefault().Supplier.Name.ToUpper(), Is.EqualTo(expectedSupplier.ToUpper()));
        }

        [Test]
        public void GetProductBySupplierCountIsEqual()
        {
            int supplierId = 1;
            var expectedSupplierCount = 2;

            Assert.That(_productService.GetProductsBySupplier(supplierId).Count(), Is.EqualTo(expectedSupplierCount));
        }
    }
}