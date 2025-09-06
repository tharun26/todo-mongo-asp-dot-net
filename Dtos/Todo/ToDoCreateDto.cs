namespace ToDo.Dtos;

public class ToDoCreateDto
{
    public string Title { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public string? AssigneeId { get; set; }
}