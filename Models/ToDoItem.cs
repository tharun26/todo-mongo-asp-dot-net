using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDo.Models;

public class ToDoItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("isCompleted")]
    public bool IsCompleted { get; set; } = false;

    [BsonElement("dueDate")]
    public DateTime? DueDate { get; set; }

    [BsonElement("assigneId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? AssigneId { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}