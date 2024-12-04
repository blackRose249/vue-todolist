using Todo.Models;

namespace WebApplication1.Repository
{
    // 3, Dao
    public interface ITodoRepository
    {
        // Add
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
