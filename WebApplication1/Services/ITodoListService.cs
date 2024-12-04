using Todo.Models;

namespace WebApplication1.Services
{
    // service
    public interface ITodoListService
    {
        public bool AddTodo(TodoList todoList);
        // Delete
        public bool DeleteTodo(string id);
        // Update
        public bool UpdateTodo(TodoList item);
        // GetAll
        public List<TodoList> GetTodoList();
        // Search
        public List<TodoList> GetSearchList(string text);
    }
}
