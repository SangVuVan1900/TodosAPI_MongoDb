using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI_1.Models;

namespace TodoAPI_1.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<Todo> _todos;

        public TodoService(ITodoApiDatabaseSettings settings) 
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _todos = database.GetCollection<Todo>(settings.TodosCollectionName);
        }      

             
        public List<Todo> Get() =>
            _todos.Find(t => t.Title.Contains("")).ToList(); 

        public Todo Get(string id) =>
            _todos.Find(todo => todo.Id == id).FirstOrDefault(); 

        public Todo Create(Todo todo)
        {
            todo.Done = false;
            _todos.InsertOne(todo);
            return todo;
        }

        public void Update(string id, string title)
        {
            var todo = _todos.Find(todo => todo.Id == id).FirstOrDefault();
            Todo todoIn = new Todo(id, title, todo.Done);
            _todos.ReplaceOne(todo => todo.Id == id, todoIn); 
        }

        public void Remove(Todo _todo) =>
            _todos.DeleteOne(todo => todo.Id == _todo.Id);

        public void Remove(string id) =>
            _todos.DeleteOne(todo => todo.Id == id);

        public void SetDone(string id)
        {
            var todo = _todos.Find(todo => todo.Id == id).FirstOrDefault();
            todo.Done = true;
            _todos.ReplaceOne(todo => todo.Id == id, todo);
        }
    }
}
