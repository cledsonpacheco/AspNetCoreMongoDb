using AspNetMongoDB.Infra;
using AspNetMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMongoDB.Controllers
{
    [Route("/api/[controller]")]
    public class ProductController : Controller
    {
        private readonly MondoDBContext _mongoDBContext;
        private IMongoCollection<ProductModel> productCollection;

        public ProductController(MondoDBContext mongoDb)
        {
            this._mongoDBContext = mongoDb;
            productCollection = this._mongoDBContext.database.GetCollection<ProductModel>("product");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = productCollection.AsQueryable<ProductModel>().ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var productId = new ObjectId(id);

            var product = productCollection.AsQueryable<ProductModel>().SingleOrDefault(x => x.Id == productId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductModel product)
        {
            try
            {
                productCollection.InsertOne(product);

                return Ok("Insert Product successfuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ProductModel product)
        {
            try
            {
                var filter = Builders<ProductModel>.Filter.Eq("_id", ObjectId.Parse(id));

                var update = Builders<ProductModel>.Update
                                    .Set("ProductName", product.ProductName)
                                    .Set("ProductDescription", product.ProductDescription)
                                    .Set("Quantity", product.Quantity);

                var result = productCollection.UpdateOne(filter, update);

                return Ok("Updated Product successfuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                productCollection.DeleteOne(Builders<ProductModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return Ok("Deleted Product successfuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
