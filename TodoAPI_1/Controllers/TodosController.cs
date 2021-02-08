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

        [HttpGet("{id:length(24)}", Name = "GetTodo")]
        public ActionResult<Todo> GetTodo(string id)
        {
            try
            {
                var todo = _todoService.Get(id);
                if (todo != null)
                {
                    return todo;
                }

                return NotFound($"Todo with id: {id} was not found, please try again!");
            }
            catch
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpPost("{id:length(24)}")]  
        public ActionResult<Todo> CreateTodo(Todo todo)
        {
            try
            {
                _todoService.Create(todo);
                return Ok("The newly created todo");
            }
            catch
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateTodo(string id, Todo todoIn) 
        {
            try
            {
                var todo = _todoService.Get(id);

                if (todo == null)
                {
                    return NotFound("Unknown todo id");
                }

                _todoService.Update(id, todoIn);
                return Ok("Successfully updated the todo");
            }
            catch
            {
                return BadRequest("Invalid data");
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
            }  
            catch
            {
                return BadRequest("Invalid data");
            }
            _todoService.Remove(todo);
            return Ok("Successfully deleted the todo"); 
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
            }
            catch
            {
                return BadRequest("Invalid data");
            }

            _todoService.SetDone(id);
            return Ok("Successfully set the todo as done");
        }
    }
}
