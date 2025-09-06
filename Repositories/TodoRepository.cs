using MongoDB.Driver;
using ToDo.Models;

namespace ToDo.Repositories;

public class ToDoRepositories : IToDoRepository
{
    private readonly IMongoCollection<ToDoItem> _collection;
    public ToDoRepositories(IMongoDatabase db)
    {
        _collection = db.GetCollection<ToDoItem>("todos");
    }
    public async Task<ToDoItem> CreateAsync(ToDoItem item, CancellationToken ct = default)
    {
        await _collection.InsertOneAsync(item, cancellationToken: ct);
        return item;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<List<ToDoItem>> GetAllAsync(CancellationToken ct = default)
    {
        var items = await _collection.FindAsync(_ => true);
        return items.ToList();
    }

    public async Task<ToDoItem> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var item = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return item;
    }

    public async Task<bool> UpdateAsync(string id, ToDoItem item, CancellationToken ct = default)
    {
        item.Id ??= id;
        var result = await _collection.ReplaceOneAsync(x => x.Id == id, item, cancellationToken: ct);
        return result.IsAcknowledged && result.ModifiedCount > 0;;
    }
}