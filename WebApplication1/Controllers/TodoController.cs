using System.Collections.Generic;
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

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<TodoList> GetById(string id)
        { 
            var item = ListData.Find(i  => i.Id == id);
            if (item == null)
                return NotFound();
            return item;
        }

        [HttpGet("search")]
        public ActionResult<List<TodoList>> GetByText([FromQuery] string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Search text is required.");
            }
            var searchList = ListData.Where(i => i.text.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            if (searchList.Count == 0)
            {
                return NotFound();
            }
            return Ok(searchList);
        }
        // createData
        [HttpPost]
        public IActionResult Create(TodoList item) 
        {
            ListData.Add(item);
            return CreatedAtRoute("GetById", new { id = item.Id }, item);
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
