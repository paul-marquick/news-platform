namespace NewsPlatform.ConsoleApp1.Entities;

public record Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public virtual List<Article>? Articles { get; set; } 
}