using Core.Data;
using Core.Domain.Catalog;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            this._productRepository = productRepository;
        }

        public virtual async Task InsertProduct(Product product)
        {
            await _productRepository.InsertAsync(product);
        }

        public virtual async Task UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            var builder = Builders<Product>.Filter;
            var filter = builder.Eq(x => x.Id, product.Id);
            var update = Builders<Product>.Update
                .Set(x => x.ProductName, product.ProductName)
                .Set(x => x.ProductDescription, product.ProductDescription)
                .Set(x => x.Quantity, product.Quantity);

            await _productRepository.Collection.UpdateOneAsync(filter, update);
        }

        public virtual async Task DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            await _productRepository.DeleteAsync(product);
        }

        public virtual async Task<Product> GetProductById(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                return null;

            return await _productRepository.GetByIdAsync(productId);
        }

        public virtual async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _productRepository.Collection.AsQueryable<Product>().ToListAsync();
        }

    }
}
