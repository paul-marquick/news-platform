using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPlatform.DataAccess;
using NewsPlatform.DataAccess.Entities;
using NewsPlatform.WebService.Mappers;

namespace NewsPlatform.WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly NewsPlatformDbContext dbContext;
    private readonly ICategoryMapper categoryMapper;

    public CategoryController(NewsPlatformDbContext dbContext, ICategoryMapper categoryMapper)
    {
        this.dbContext = dbContext;
        this.categoryMapper = categoryMapper;
    }

    [HttpGet]
    public IEnumerable<DTOs.Category> Get()
    {
        return dbContext.Categories.OrderBy(x => x.Name).Select(categoryMapper.MapEntityToDto);
    }

    [HttpGet("{id}")]
    public async Task<DTOs.Category?> GetAsync(Guid id)
    {
        var categoryEntity = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        if (categoryEntity == null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;

            return null;
        }
        else
        {
            return categoryMapper.MapEntityToDto(categoryEntity);
        }
    }

    [HttpPost]
    public async Task<DTOs.Category> PostAsync([FromBody] DTOs.Category categoryDto)
    {
        Category categoryEntity = new Category
        {
            Name = categoryDto.Name
        };

        dbContext.Categories.Add(categoryEntity);
        await dbContext.SaveChangesAsync();

        categoryDto.Id = categoryEntity.Id;

        Response.StatusCode = StatusCodes.Status201Created;

        return categoryDto;
    }

    [HttpPut("{id}")]
    public async Task PutAsync(Guid id, [FromBody] DTOs.Category categoryDto)
    {
        var categoryEntity = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        if (categoryEntity == null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            categoryMapper.UpdateEntityWithDto(categoryEntity, categoryDto);

            dbContext.Categories.Update(categoryEntity);
            await dbContext.SaveChangesAsync();

            Response.StatusCode = StatusCodes.Status204NoContent;
        }
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        Category? category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        if (category != null)
        {
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        Response.StatusCode = StatusCodes.Status204NoContent;
    }
}