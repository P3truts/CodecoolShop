using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDao : IDao<Product>
    {
        IEnumerable<Product> GetBySupplier(Supplier supplier);
        IEnumerable<Product> GetByCategory(ProductCategory productCategory);
    }
}
