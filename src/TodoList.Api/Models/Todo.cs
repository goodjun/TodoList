using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Api.Models;

public class Todo
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; }
}