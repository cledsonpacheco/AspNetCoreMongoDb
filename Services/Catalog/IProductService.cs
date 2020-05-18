using Core.Domain.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Catalog
{
    public interface IProductService
    {
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);

        Task<Product> GetProductById(string productId);
        Task<IEnumerable<Product>> GetAllProduct();
    }
}
