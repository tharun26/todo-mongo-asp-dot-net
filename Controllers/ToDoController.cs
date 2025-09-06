using Microsoft.AspNetCore.Mvc;
using ToDo.Dtos;
using ToDo.Models;
using ToDo.Repositories;

[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    // Controller actions go here
    private readonly IToDoRepository _todoRepo;
    private readonly IUserRepository _userRepo;

    public ToDoController(IToDoRepository todoRepo, IUserRepository userRepo)
    {
        _todoRepo = todoRepo;
        _userRepo = userRepo;
    }

    // GET: api/todo
    [HttpGet]
    public async Task<ActionResult<List<ToDoViewDto>>> GetAll(CancellationToken ct)
    {
        var items = await _todoRepo.GetAllAsync(ct);
        var views = new List<ToDoViewDto>(items.Count);
        foreach (var t in items) views.Add(await MapToViewAsync(t, ct));
        return Ok(views);
    }

    // GET: api/todo/:id
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItem>> GetById(string id, CancellationToken ct)
    {
        var item = await _todoRepo.GetByIdAsync(id, ct);
        return Ok(await MapToViewAsync(item, ct));
    }

    // POST: api/todo
    [HttpPost]
    public async Task<ActionResult<ToDoItem>> Create([FromBody]ToDoCreateDto dto, CancellationToken ct)
    {
        var entity = new ToDoItem
        {
            Title = dto.Title,
            IsCompleted = false,
            DueDate = dto.DueDate,
            AssigneId = dto.AssigneeId,
            CreatedAt = DateTime.UtcNow
        };

        var saved = await _todoRepo.CreateAsync(entity, ct);
        return CreatedAtAction(nameof(GetById), new { id = saved.Id }, saved);
    }

    // PUT: api/todo
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody]ToDoUpdateDto dto, CancellationToken ct)
    {
        var existing = await _todoRepo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.Title = dto.Title;
        existing.DueDate = dto.DueDate;
        existing.IsCompleted = dto.IsCompleted;

        var result = await _todoRepo.UpdateAsync(id, existing, ct);
        if (!result)
        {
            return Problem("Update was not acknowledged by MongoDb");
        }
        return NoContent();
    }

    //Delete: api/todo
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id, CancellationToken ct)
    {
        var result = await _todoRepo.DeleteAsync(id, ct);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

private async Task<ToDoViewDto> MapToViewAsync(ToDoItem t, CancellationToken ct)
{
    AssigneeDto? assignee = null;
    if (!string.IsNullOrWhiteSpace(t.AssigneId))
    {
        var u = await _userRepo.GetById(t.AssigneId!, ct);
        if (u is not null)
        {
            assignee = new AssigneeDto { Id = u.Id!, Name = u.Name, Email = u.Email };
        }
    }

    return new ToDoViewDto
    {
        Id = t.Id!,
        Title = t.Title,
        IsCompleted = t.IsCompleted,
        DueDate = t.DueDate,
        CreatedAt = t.CreatedAt,
        Assignee = assignee
    };
}

}

