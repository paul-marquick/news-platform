namespace NewsPlatform.DTOs;

public record Article
{
    public Article(Guid id, string title, string content, Guid categoryId)
    {
        Id = id;
        Title = title;
        Content = content;
        CategoryId = categoryId;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid CategoryId { get; set; }
}