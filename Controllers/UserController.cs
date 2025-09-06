

using Microsoft.AspNetCore.Mvc;
using ToDo.Dtos;
using ToDo.Models;
using ToDo.Repositories;

[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;
    public UserController(IUserRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetAllUser(CancellationToken ct)
    {
        var result = await _repo.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(string id, CancellationToken ct)
    {
        var result = await _repo.GetById(id, ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody]UserCreateDto dto, CancellationToken ct)
    {
        var entity = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow
        };
        var result = await _repo.Create(entity, ct);
        return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateUser(string id, [FromBody]UserUpdateDto dto, CancellationToken ct)
    {
        var currentUser = await _repo.GetById(id, ct);
        currentUser.Name = dto.Name;
        currentUser.Email = dto.Email;
        var result = await _repo.Update(id, currentUser, ct);
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteAsync(string id, CancellationToken ct)
    {
        var result = await _repo.Delete(id, ct);
        if (!result) return NotFound();
        return NoContent();
    }

}