using MongoDB.Driver;
using ToDo.Models;

namespace ToDo.Repositories;

public class UserRepositories : IUserRepository
{
    private readonly IMongoCollection<User> _collection;
    public UserRepositories(IMongoDatabase db)
    {
        _collection = db.GetCollection<User>("users");
    }

    public async Task<User> Create(User user, CancellationToken ct = default)
    {
       await _collection.InsertOneAsync(user, cancellationToken: ct);
       return user;
    }
    public async Task<bool> Delete(string id, CancellationToken ct = default)
    {
        var result = await _collection.DeleteOneAsync<User>(x => x.Id == id, cancellationToken: ct);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<List<User>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _collection.Find(_ => true).ToListAsync();
        return result;
    }

    public async Task<User> GetById(string id, CancellationToken ct = default)
    {
        var result = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return result;
    }

    public async Task<bool> Update(string id, User user, CancellationToken ct = default)
    {

        var result = await _collection.ReplaceOneAsync(x => x.Id == id, user, cancellationToken: ct);
        return result.IsAcknowledged && result.ModifiedCount > 0 ;
    }
}