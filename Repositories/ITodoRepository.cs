
using ToDo.Models;

namespace ToDo.Repositories;


public interface IToDoRepository
{
    Task<List<ToDoItem>> GetAllAsync(CancellationToken ct = default);
    Task<ToDoItem> GetByIdAsync(string id, CancellationToken ct = default);
    Task<ToDoItem> CreateAsync(ToDoItem item, CancellationToken ct = default);
    Task<bool> UpdateAsync(string id, ToDoItem item, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
}