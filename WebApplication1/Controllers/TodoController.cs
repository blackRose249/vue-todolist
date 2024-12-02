using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoList> ListData = new List<TodoList>()  {
            new TodoList() { Id = "1", text = @"running", IsEditing = false, },
            new TodoList() { Id = "2", text = @"swimming", IsEditing = false, },
            new TodoList() { Id = "3", text = @"flying", IsEditing = false, }
        };
        // getData
        [HttpGet]
        public ActionResult<List<TodoList>> GetAll() => ListData;

        [HttpGet("{id}", Name = "WebApplication1")]
        public ActionResult<TodoList> GetById(string id)
        { 
            var item = ListData.Find(i  => i.Id == id);
            if (item == null)
                return NotFound();
            return item;
        }
        // createData
        [HttpPost]
        public IActionResult Create(TodoList item) 
        {
            ListData.Add(item);
            return CreatedAtRoute("Todo", item);
        }
        // updateData
        [HttpPut("{id}")]
        public IActionResult Update(string id, TodoList item) 
        {
            var target = ListData.Find(i => i.Id == id);
            if (target == null)
                return NotFound();
            target.text = item.text;
            target.IsEditing = item.IsEditing;

            return NoContent();
        }
        // deleteData
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var n = ListData.RemoveAll(i => i.Id == id);
            if (n == 0)
                return NotFound();
            return NoContent();
        }
    }
}
