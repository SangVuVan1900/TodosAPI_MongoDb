using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI_1.Models
{
    public class Todo
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired] 
        [MinLength(1)]
        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonRequired]
        public bool Done { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now.AddHours(7);
          
        public DateTime? UpdatedDate { get; set; } = null; 

        public Todo()
        {
        }
         
        public Todo(string Id, string Title, bool Done, DateTime CreatedDate, DateTime UpdatedDate)
        {
            this.Id = Id; 
            this.Title = Title;
            this.Done = Done; 
            this.CreatedDate = CreatedDate;
            this.UpdatedDate = UpdatedDate;
        } 
    }
}