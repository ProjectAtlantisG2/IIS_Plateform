using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace DAO
{
    public class MongoCollection
    {
        public MongoCollection(DateTime date, float value)
        {
            this.Date = date;
            this.Value = value;
        }

        public MongoCollection(ObjectId id, DateTime date, float value)
        {
            this._id = id;
            this.Date = date;
            this.Value = value;
        }

        public ObjectId _id { get; set; }

        public DateTime Date { get; set; }
        public float Value { get; set; }
        

    }
    
}
