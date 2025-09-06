namespace ToDo.Dtos;


public class ToDoUpdateDto
{
    public string Title { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public string? AssigneeId { get; set; }
}