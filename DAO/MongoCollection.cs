using System;
using System.Collections.Generic;
using System.Text;

namespace DAO
{
    public class MongoCollection
    {
        public MongoCollection(DateTime date, float value)
        {
            this.Date = date;
            this.Value = value;
        }

        public DateTime Date { get; set; }
        public float Value { get; set; }
        

    }
    
}
