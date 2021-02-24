using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI_1.Models;
using static TodoAPI_1.Models.Params;

namespace TodoAPI_1.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<Todo> _todos;
        int PageSize = 3;
        public TodoService(ITodoApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _todos = database.GetCollection<Todo>(settings.TodosCollectionName);
        }

        public List<Todo> Get() =>
            _todos.Find(t => t.Title.Contains("")).ToList();

        public Todo Get(string id)
        {
            var todo = _todos.Find(todo => todo.Id == id).FirstOrDefault();
            if (todo == null)
            {
                return null;
            }
            return todo;
        }

        public Todo Create(Todo todo)
        {
            _todos.InsertOne(todo);
            return todo;
        }

        public void Update(string id, string title)
        {
            var todo = _todos.Find(todo => todo.Id == id).FirstOrDefault();
            if (todo == null)
            {
                return;
            }
            Todo todoIn = new Todo(id, title, todo.Done, todo.CreatedDate, DateTime.Now.AddHours(7));
            _todos.ReplaceOne(todo => todo.Id == id, todoIn);
        }

        public void Remove(Todo _todo) =>
            _todos.DeleteOne(todo => todo.Id == _todo.Id);

        public void Remove(string id) =>
            _todos.DeleteOne(todo => todo.Id == id);

        public void SetDone(string id)
        {
            var todo = _todos.Find(todo => todo.Id == id).FirstOrDefault();
            if (todo == null)
            {
                return;
            }
            todo.Done = true;
            _todos.ReplaceOne(todo => todo.Id == id, todo);
        }
<<<<<<< HEAD
         
        public List<Todo> Searching(Params p)
        {
            List<Todo> pages = new List<Todo>();
            if (p.SortByAscending == false) 
            {
                switch (p.SortType)
                {
                    case "title":
                        pages = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                           .SortBy(t => t.Title)
                           .Skip(p.PageNumber * PageSize)
                           .Limit(PageSize)
                           .ToList(); 
                        break;
                    case "createdDate":
                        pages = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                           .SortBy(t => t.CreatedDate)
                           .Skip(p.PageNumber * PageSize)
                           .Limit(PageSize)
                           .ToList();
                        break;
=======

         public List<Todo> Searching(Params p)
        {
            if (string.IsNullOrEmpty(p.Title))
            {
                p.Title = "";
            }
            if (p.SortByAscending)
            {
                var pagesAscending = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                    .SortBy(t => t.Title)
                    .SortBy(t => t.CreatedDate)
                    .SortBy(t => t.UpdatedDate)
                    .Skip(p.PageNumber * p.PageSize)
                    .Limit(p.PageSize);
>>>>>>> a8de92ef2ea522022db330434e5cc44719b43ef1

                    case "updatedDate":
                        pages = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                           .SortBy(t => t.UpdatedDate)
                           .Skip(p.PageNumber * PageSize)
                           .Limit(PageSize)
                           .ToList();
                        break;
                }
            } 
            else
            {
<<<<<<< HEAD
                switch (p.SortType)
                {
                    case "title":
                        pages = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                           .SortByDescending(t => t.Title)
                           .Skip(p.PageNumber * PageSize)
                           .Limit(PageSize)
                           .ToList();
                        break;
                    case "createdDate":
                        pages = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                           .SortByDescending(t => t.CreatedDate)
                           .Skip(p.PageNumber * PageSize)
                           .Limit(PageSize)
                           .ToList();
                        break;
                         
                    case "updatedDate":
                        pages = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                           .SortByDescending(t => t.UpdatedDate)
                           .Skip(p.PageNumber * PageSize)
                           .Limit(PageSize)
                           .ToList();
                        break;
                }
            } 
=======
                var pagesDescending = _todos.Find(t => t.Done == p.Done && t.Title.Contains(p.Title))
                    .SortByDescending(t => t.Title)
                    .SortByDescending(t => t.CreatedDate)
                    .SortByDescending(t => t.UpdatedDate)
                    .Skip(p.PageNumber * p.PageSize)
                    .Limit(p.PageSize);
>>>>>>> a8de92ef2ea522022db330434e5cc44719b43ef1

            return pages;
        }
    }
}
