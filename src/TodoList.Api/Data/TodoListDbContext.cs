using Microsoft.EntityFrameworkCore;
using TodoList.Api.Models;

namespace TodoList.Api.Data;

public class TodoListDbContext : DbContext
{
    public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; } = null!;
}