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

        public DateTime CreatedDate { get; set; } = DateTime.Now;
         
        public DateTime? UpdatedDate { get; set; } = null; 

        public Todo()
        {
        }
         
<<<<<<< HEAD
        public Todo(string Id, string Title, bool Done, DateTime CreatedDate, DateTime UpdatedDate)
=======
        public Todo(string Id, string Title, bool Done)
>>>>>>> 114587641e784424d13916f741f8b8e7d40d1a70
        {
            this.Id = Id; 
            this.Title = Title;
<<<<<<< HEAD
            this.Done = Done; 
            this.CreatedDate = CreatedDate;
            this.UpdatedDate = UpdatedDate;
        } 
=======
            this.Done = Done;
        }


>>>>>>> 114587641e784424d13916f741f8b8e7d40d1a70
    }
}
