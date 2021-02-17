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

        [Required]
        [MinLength(1)]
        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonRequired]
        public bool Done { get; set; } = false;

        public Todo()
        {
        }

        public Todo(string Id, string Title)
        {
            this.Id = Id;
            this.Title = Title;
        }


    }
}