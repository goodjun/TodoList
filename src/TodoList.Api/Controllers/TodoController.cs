using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoListDbContext _context;

        public TodoController(TodoListDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取todo list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            return await _context.Todos.ToListAsync();
        }
        
        /// <summary>
        /// 获取todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(long id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        /// <summary>
        /// 更新todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(long id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        /// <summary>
        /// 创建todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TodoExists(todo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        /// <summary>
        /// 删除todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(long id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(long id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
