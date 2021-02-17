using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI_1.Models;
using TodoAPI_1.Services;

namespace TodoAPI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodosController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
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
            return Ok("The newly created todo");
        }


        

        [HttpGet("{todoId:length(0, 24)}", Name = "GetTodo")]
        public ActionResult<Todo> GetTodo(string todoId)
        {
            try
            {
                var todo = _todoService.Get(todoId);
                if (todo == null)
                {
                    return NotFound("Unknown todo id");
                }

                return todo;
            }
            catch
            {
                return NotFound("Unknown todo id");
            }
        }

        [HttpPut("{todoId:length(0, 24)}")]
        public IActionResult UpdateTodo(string todoId, [FromBody] string update)
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
                return Ok("Successfully updated the todo");
            }
            catch
            {
                return NotFound("Unknown todo id");
            }
        }


     
        [HttpDelete]
        public IActionResult DeleteTodo(string id)
        {
            Todo todo;
            try
            {
                todo = _todoService.Get(id);
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
    }
}
