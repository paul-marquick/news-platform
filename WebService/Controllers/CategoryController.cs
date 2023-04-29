using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    public ActionResult<IEnumerable<DTOs.Category>> Get()
    {
        return Ok(dbContext.Categories.OrderBy(x => x.Name).Select(categoryMapper.MapEntityToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DTOs.Category>> GetAsync(Guid id)
    {
        var categoryEntity = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        if (categoryEntity == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(categoryMapper.MapEntityToDto(categoryEntity));
        }
    }

    [HttpPost]
    public async Task<ActionResult<DTOs.Category>> PostAsync([FromBody] DTOs.Category category)
    {
        Category categoryEntity = new Category
        {
            Name = category.Name
        };

        dbContext.Categories.Add(categoryEntity);
        await dbContext.SaveChangesAsync();

        category.Id = categoryEntity.Id;

        return Created($"https://category.example.com/{category.Id}", category);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(Guid id, [FromBody] DTOs.Category category)
    {
        var categoryEntity = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        if (categoryEntity == null)
        {
            return NotFound();
        }
        else
        {
            categoryMapper.UpdateEntityWithDto(categoryEntity, category);

            dbContext.Categories.Update(categoryEntity);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        Category? category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        if (category != null)
        {
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        return NoContent();
    }
}