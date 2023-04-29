using System.ComponentModel.DataAnnotations;

namespace NewsPlatform.DTOs;

public record Category
{
    public Category(
        Guid id, 
        [Required] string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}