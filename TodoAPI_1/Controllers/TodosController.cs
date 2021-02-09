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
        // up
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

        [HttpPost]
        public ActionResult<Todo> CreateTodo(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            _todoService.Create(todo);
            return Ok("The newly created todo");
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateTodo(string id, Todo todoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data"); 
            }

            var todo = _todoService.Get(id); 
            if (todo != null)
            {
                _todoService.Update(id, todoIn); 
            }
            else
            {
                return NotFound("Unknown todo id");
            }

            return Ok("Successfully updated the todo");
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
