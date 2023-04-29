namespace NewsPlatform.DataAccess.Entities;

public record Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public virtual List<Article>? Articles { get; set; } 
}