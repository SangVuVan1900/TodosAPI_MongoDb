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

        int pageSize = 2;
        public List<Todo> Searching(bool done, string title, int page)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "";
            }
            var pages = _todos
                .Find(t => t.Done == done && t.Title.Contains(title))
                .Skip(page * pageSize)
                .Limit(pageSize);

            return pages.ToList();
        }

        public List<Todo> Sorting(bool isAscending)
        {
            List<Todo> todos = _todos.Find(t => t.Title.Contains("")).ToList();

            if (isAscending)
            {
                var todosSorting = todos.OrderBy(t => t.Title)
                .ThenBy(t => t.CreatedDate)
                .ThenBy(t => t.UpdatedDate)
                .ToList();
                return todosSorting;
            }
            else
            {
                var todosSorting = todos.OrderByDescending(t => t.Title)
                .ThenByDescending(t => t.CreatedDate)
                .ThenByDescending(t => t.UpdatedDate)
                .ToList();
                return todosSorting;
            }
        }
        //public List<Todo> Searching(bool done, string title, int PageNumber, int PageSize)
        //{
        //    List<Todo> todos = _todos.Find(t => t.Title.Contains("")).ToList();

        //    if (string.IsNullOrEmpty(title))
        //    {
        //        title = "";
        //    }

        //    TodoPagination todoPagination = new TodoPagination(PageNumber, PageSize);
        //    return todos.FindAll(t => t.Done == done && t.Title.Contains(title))
        //        .Skip((todoPagination.PageNumber - 1) * todoPagination.PageSize)
        //        .Take(todoPagination.PageSize)
        //        .ToList();
        //}
    }
}