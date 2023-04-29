namespace NewsPlatform.DataAccess.Entities;

public record Article
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required Guid CategoryId { get; set; }

    public virtual Category Category { get; set; }
}