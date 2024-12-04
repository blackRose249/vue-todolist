using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace WebApplication1.Contexts
{
    public class TodoLIstDbContext : DbContext
    {
        public TodoLIstDbContext(DbContextOptions options) :base(options) { 
        }
  
        public DbSet<TodoList> TodoLists { get; set; }
    }
}
