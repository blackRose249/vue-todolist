using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api2/[controller]")]
    [ApiController]
    // 1, Controller
    public class TodoMySqlController : ControllerBase
    {
        // Use Service
        public ITodoListService TodoListService;

        public TodoMySqlController(ITodoListService todoListService)
        {
            TodoListService = todoListService;
        }

        [HttpPost(Name = "AddTodoItem")]
        public bool AddTodoItem(TodoList todoList) {
            return TodoListService.AddTodo(todoList);
        }

        [HttpDelete("{id}", Name = "DeleteTodoItem")]
        public bool DeleteTodoItem(string id) {
            return TodoListService.DeleteTodo(id);
        }

        [HttpPut(Name = "UpdateTodoItem")]
        public bool UpdateTodoItem(TodoList item) 
        {
            return TodoListService.UpdateTodo(item);
        }

        [HttpGet("list", Name = "GetTodoList")]
        public ActionResult<List<TodoList>> GetTodoList()
        {
            return TodoListService.GetTodoList();
        }

        [HttpGet("search", Name = "GetSearchList")]
        public ActionResult<List<TodoList>> GetByText([FromQuery] string text) { 
            return TodoListService.GetSearchList(text);
        }
    }
}
