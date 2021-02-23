using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TodoAPI_1.Models;
using TodoAPI_1.Services;

namespace TodoAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        //db.Todos.insert({'Title':'Design Patterns','Done': false,'CreatedDate': new Date('2020-12-12T13:58:51'), 'UpdatedDate': new Date('2021-01-01-T17:11;01')})

        //var modelState = new ModelStateDictionary();
        //modelState.AddModelError("message", "Invalid data");
        //return BadRequest(modelState);

        private readonly TodoService _todoService;
        private readonly ILogger<TodosController> logger;


        public TodosController(TodoService todoService, ILogger<TodosController> logger)
        {
            _todoService = todoService;
            this.logger = logger;
        }

        [HttpGet(Name = "GetTodos")]
        public ActionResult<List<Todo>> GetTodos() =>
            _todoService.Get();

        [HttpPost]  
        public ActionResult<Todo> CreateTodo([FromBody] string todo)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(todo))
            {
                return BadRequest("Invalid data");
            }
            Todo todoRecord = new Todo();
            todoRecord.Title = todo;

            _todoService.Create(todoRecord);
            return todoRecord;
        }

        [HttpGet("{todoId:length(0, 24)}", Name = "GetTodo")]
        public ActionResult<Todo> GetTodo(string todoId)
        {
            logger.LogDebug("get by Id method executing...");
            try
            {
                var todo = _todoService.Get(todoId);
                if (todo == null)
                {
                    logger.LogWarning($"Todo with Id: {todoId} not found");
                    logger.LogError("This is an error");
                    return NotFound("Unknown todo id");
                }

                logger.LogInformation($"Return todo with Id: {todoId}");
                return todo;
            }
            catch
            {
                logger.LogWarning($"Todo with Id: {todoId} not found");
                return NotFound("Unknown todo id");
            }
        }

        //[Route("{todoId}")]
        [HttpPut("{todoId:length(0, 24)}")]
        public ActionResult<Todo> UpdateTodo(string todoId, [FromBody] string update)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(update))
            {
                return BadRequest("Invalid data");
            }
            try 
            {
                var todo = _todoService.Get(todoId);
                if (todo == null)
                {
                    return NotFound("Unknown todo id");
                }

                _todoService.Update(todoId, update);
                Todo todoUpdate = new Todo(todoId, update, todo.Done, todo.CreatedDate, DateTime.Now);
                return todoUpdate;
            }
            catch
            {
                return NotFound("Unknown todo id");
            }
        }

        [HttpDelete]
        public IActionResult DeleteTodo(string todoId)
        {
            Todo todo;
            try
            {
                todo = _todoService.Get(todoId);
                if (todo == null)
                {
                    return NotFound("Unknown todo id");
                }
                _todoService.Remove(todo);
                return Ok("Successfully deleted the todo");
            }
            catch
            {
                return NotFound("Unknown todo id");
            }
        }

        [HttpPatch]
        public IActionResult SetDone(string id)
        {
            try
            {
                var todo = _todoService.Get(id);

                if (todo == null)
                {
                    return NotFound("Unknown todo id");
                }
                _todoService.SetDone(id);
                return Ok("Successfully set the todo as done");
            }
            catch
            {
                return NotFound("Unknown todo id");
            }
        }

        [HttpGet("SearchTodos")]
        public ActionResult<List<Todo>> SearchTodos(bool done, string title, int page)
        {
            try 
            {
                return _todoService.Searching(done, title, page);
            }
            catch
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpGet("SortTodos")]
        public ActionResult<List<Todo>> SortTodos(bool isAscending) =>
            _todoService.Sorting(isAscending);



    }
}