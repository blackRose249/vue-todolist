using Microsoft.EntityFrameworkCore;
using Todo.Models;
using WebApplication1.Contexts;

namespace WebApplication1.Repository
{
    public class TodoRepository : ITodoRepository
    {
        public TodoLIstDbContext Context;

        public TodoRepository(TodoLIstDbContext context)
        {
            Context = context;
        }

        public bool AddTodo(TodoList todoList)
        {
            // use Context
            if (todoList == null) 
            {
                throw new ArgumentException(nameof(todoList), "TodoList cannot be null");
            }
            Context.TodoLists.Add(todoList);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected > 0;
        }
        public bool DeleteTodo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Todo ID cannot be null or empty.", nameof(id));
            }
            var todoList = Context.TodoLists.FirstOrDefault(t => t.Id == id);
            if (todoList == null)
            {
                return false;
            }
            Context.TodoLists.Remove(todoList);
            int rowsAffected = Context.SaveChanges();
            return rowsAffected > 0;
        }
        public bool UpdateTodo(TodoList todoList) {
            if (todoList == null) return false;
            var flag = Context.TodoLists.FirstOrDefault(t => t.Id == todoList.Id);
            if (flag == null) return false;

            flag.text = todoList.text;
            flag.IsEditing = todoList.IsEditing;
            Context.TodoLists.Update(flag);
            Context.SaveChanges();

            return true;
        }
        public List<TodoList> GetTodoList() {
            return Context.TodoLists.ToList();
        }
        public List<TodoList> GetSearchList(string text) {
            return Context.TodoLists.Where(todos => todos.text.StartsWith(text)).ToList();
        }
    }
}
