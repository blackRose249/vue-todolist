using Todo.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    // 2, Service
    public class TodoListService : ITodoListService
    {
        // Use DAO
        public ITodoRepository TodoRepository;

        public TodoListService(ITodoRepository todoRepository) { 
            TodoRepository = todoRepository;
        }

        public bool AddTodo(TodoList todoList)
        {
            return TodoRepository.AddTodo(todoList);
        }

        public bool DeleteTodo(string id) {
            return TodoRepository.DeleteTodo(id);
        }
        public bool UpdateTodo(TodoList item) { 
            return TodoRepository.UpdateTodo(item);
        }
        public List<TodoList> GetTodoList() { 
            return TodoRepository.GetTodoList();
        }
        public List<TodoList> GetSearchList(string text) { 
            return TodoRepository.GetSearchList(text);
        }
    }
}
