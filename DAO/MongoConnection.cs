using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Configuration;

namespace DAO
{
    public class MongoConnection
    {
        private MongoClient MongoClient { get; set; }
        private IMongoDatabase Database { get; set; }
        private IMongoCollection<ComplexData> Collection { get; set; }

        public MongoConnection(string connectionString, string databaseName, string collectionName = null)
        {
            try
            {
                this.MongoClient = new MongoClient(connectionString);
                this.Database = this.MongoClient.GetDatabase(databaseName);
                this.Collection = this.Database.GetCollection<ComplexData>(collectionName);
            }
            catch { return; }
        }

        public void InsertData(string collectionName, float data)
        {
            var collection = this.Database.GetCollection<ComplexData>(collectionName);
            //collection.InsertOne(data);
        }
    }
}
