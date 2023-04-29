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
    public IEnumerable<Models.Category> Get()
    {
        return dbContext.Categories.OrderBy(x => x.Name).Select(categoryMapper.MapEntityToModel);
    }

    [HttpGet("{id}")]
    public async Task<Models.Category?> GetAsync(Guid id)
    {
        var categoryEntity = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        if (categoryEntity == null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;

            return null;
        }
        else
        {
            return categoryMapper.MapEntityToModel(categoryEntity);
        }
    }

    [HttpPost]
    public async Task<Models.Category> PostAsync([FromBody] Models.Category categoryModel)
    {
        Category categoryEntity = categoryMapper.MapModelToEntity(categoryModel);

        dbContext.Categories.Add(categoryEntity);
        await dbContext.SaveChangesAsync();

        categoryModel.Id = categoryEntity.Id;

        Response.StatusCode = StatusCodes.Status201Created;

        return categoryModel;
    }

    [HttpPut("{id}")]
    public async Task PutAsync(Guid id, [FromBody] Models.Category categoryModel)
    {
        Category categoryEntity = categoryMapper.MapModelToEntity(categoryModel);
        categoryEntity.Id = id;

        dbContext.Categories.Update(categoryEntity);
        await dbContext.SaveChangesAsync();

        Response.StatusCode = StatusCodes.Status204NoContent;
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