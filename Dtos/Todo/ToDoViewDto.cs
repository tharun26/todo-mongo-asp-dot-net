namespace ToDo.Dtos;


public class ToDoViewDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public AssigneeDto? Assignee { get; set; }
}