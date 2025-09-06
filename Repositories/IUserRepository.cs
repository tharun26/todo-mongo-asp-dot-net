using ToDo.Models;

namespace ToDo.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(CancellationToken ct = default);
    Task<User> GetById(string id, CancellationToken ct = default);
    Task<User> Create(User user, CancellationToken ct = default);
    Task<bool> Update(string id, User user, CancellationToken ct = default);
    Task<bool> Delete(string id, CancellationToken ct = default);
}