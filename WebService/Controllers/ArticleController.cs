using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPlatform.DataAccess;
using NewsPlatform.DataAccess.Entities;
using NewsPlatform.WebService.Mappers;

namespace NewsPlatform.WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly NewsPlatformDbContext dbContext;
    private readonly IArticleMapper articleMapper;
    private readonly ILogger<ArticleController> logger;

    public ArticleController(NewsPlatformDbContext dbContext, IArticleMapper articleMapper, ILogger<ArticleController> logger)
    {
        this.dbContext = dbContext;
        this.articleMapper = articleMapper;
        this.logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DTOs.Article>> Get()
    {
        return Ok(dbContext.Articles.OrderBy(x => x.Title).Select(articleMapper.MapEntityToDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DTOs.Article>> GetAsync(Guid id)
    {
        var articleEntity = await dbContext.Articles.SingleOrDefaultAsync(x => x.Id == id);

        if (articleEntity == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(articleMapper.MapEntityToDto(articleEntity));
        }
    }

    [HttpPost]
    public async Task<ActionResult<DTOs.Article>> PostAsync([FromBody] DTOs.Article article)
    {
        logger.LogDebug($"PostAsync, article.Title: {article.Title}, article.Content: {article.Content}, article.CategoryId: {article.CategoryId}");

        Article articleEntity = new Article
        {
            Title = article.Title,
            Content = article.Content,
            CategoryId = article.CategoryId
        };

        try
        {
            dbContext.Articles.Add(articleEntity);
            await dbContext.SaveChangesAsync();

            article.Id = articleEntity.Id;

            return Created($"https://article.example.com/{article.Id}", article);
        }
        catch (DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException!.Message.Contains("FK_Article_Category_CategoryId", StringComparison.OrdinalIgnoreCase))
            {
                ValidationProblemDetails validationProblemDetails = CreateValidationProblemDetails(article.CategoryId);

                return BadRequest(validationProblemDetails);
            }
            else
            {
                throw;
            }
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(Guid id, [FromBody] DTOs.Article article)
    {
        logger.LogDebug($"PutAsync, id: {id}, article.Title: {article.Title}, article.Content: {article.Content}, article.CategoryId: {article.CategoryId}");

        var articleEntity = await dbContext.Articles.SingleOrDefaultAsync(x => x.Id == id);

        if (articleEntity == null)
        {
            return NotFound();
        }
        else
        {
            articleMapper.UpdateEntityWithDto(articleEntity, article);

            try
            { 
                dbContext.Articles.Update(articleEntity);
                await dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("FK_Article_Category_CategoryId", StringComparison.OrdinalIgnoreCase))
                {
                    ValidationProblemDetails validationProblemDetails = CreateValidationProblemDetails(article.CategoryId);

                    return BadRequest(validationProblemDetails);
                }
                else
                {
                    throw;
                }
            }
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        logger.LogDebug($"DeleteAsync, id: {id}.");

        Article? article = await dbContext.Articles.SingleOrDefaultAsync(x => x.Id == id);

        if (article != null)
        {
            dbContext.Articles.Remove(article);
            await dbContext.SaveChangesAsync();
        }

        return NoContent();
    }

    #region Private

    private static ValidationProblemDetails CreateValidationProblemDetails(Guid categoryId)
    {
        return new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "category_not_found",
            Title = "Category does not exist",
            Detail = $"Category with id of {categoryId} does not exist"
        };
    }

    #endregion Private
}