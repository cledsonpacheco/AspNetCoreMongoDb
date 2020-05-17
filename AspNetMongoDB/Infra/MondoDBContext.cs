using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AspNetMongoDB.Infra
{
    public class MondoDBContext
    {
        public IMongoDatabase database;

        public MondoDBContext(IConfiguration configuration)
        {
            var mongoSection = configuration.GetSection("MongoDB");
            var mongoHost = mongoSection.GetValue<string>("DBHost");
            var mongoDbName = mongoSection.GetValue<string>("DBName");

            var mongoCliente = new MongoClient(mongoHost);
            this.database = mongoCliente.GetDatabase(mongoDbName); 
        }
    }
}
