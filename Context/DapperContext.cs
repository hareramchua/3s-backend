using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Data;
using DotNetEnv;

namespace _5s.Context
{
    /// <summary>
    /// Wrapper class for database context.
    /// </summary>
    public class DapperContext
    {
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Create new instance of DapperContext.
        /// </summary>
        /// <param name="configuration">Contains MongoDB connection string and database name</param>
        public DapperContext(IConfiguration configuration)
        {
            // string envFilePath = "/home/russelharvey/Documents/school/soft-eng/fiveSAi-master/env/mongo.env";
            string envFilePath = "./env/mongo.env";
            DotNetEnv.Env.Load(envFilePath);

            string mongodbPassword = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD");
            string mongodbUsername = Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_USERNAME");

            // Construct the MongoDB connection string
            var connectionString = configuration.GetConnectionString("MongoDB")
                .Replace("<username>", mongodbUsername)
                .Replace("<password>", mongodbPassword);

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("FiveSDB");
        }


        /// <summary>
        /// Get the MongoDB database instance.
        /// </summary>
        /// <returns>The MongoDB database instance</returns>
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
