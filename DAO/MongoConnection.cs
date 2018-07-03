using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Configuration;
using MongoDB.Bson;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace DAO
{
    public class MongoConnection
    {
        private MongoClient MongoClient { get; set; }
        private IMongoDatabase Database { get; set; }

        public MongoConnection(string connectionString, string databaseName)
        {
            try
            {
                this.MongoClient = new MongoClient(connectionString);
                this.Database = this.MongoClient.GetDatabase(databaseName);
            }
            catch { return; }
        }

        public void InsertData(string collectionName, float data, DateTime from, Delay delay)
        {
            var collection = this.Database.GetCollection<MongoCollection>(collectionName);

            var doc = new MongoCollection(from, data);
            
            if (doc != null) collection.InsertOne(doc);
        }

        public string GetData(string collectionName, DateTime from, DateTime to, Delay delay)
        {
            var collection = this.Database.GetCollection<MongoCollection>(collectionName);

            var filterBuilder = Builders<MongoCollection>.Filter;
            var filter = filterBuilder.Gte(x => x.Date, new BsonDateTime(from)) & filterBuilder.Lte(x => x.Date, new BsonDateTime(to));
            List<MongoCollection> searchResult = collection.Find(filter).ToList();
            var serializedResult = Newtonsoft.Json.JsonConvert.SerializeObject(searchResult);
            return serializedResult;
        }
    }
}
