using NewsPlatform.ConsoleApp1.Entities;

namespace NewsPlatform.ConsoleApp1;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new NewsPlatformContext();

        Console.Write("Enter a name for a new category:");
        var name = Console.ReadLine();

        var category = new Category { Name = name };
        db.Categories.Add(category);
        db.SaveChanges();

        var query = from c in db.Categories
                    orderby c.Name
                    select c;

        foreach (var item in query)
        {
            Console.WriteLine(item.Name);
        }
    }
}