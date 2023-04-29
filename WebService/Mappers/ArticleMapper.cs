namespace NewsPlatform.WebService.Mappers;

public class ArticleMapper : IArticleMapper
{
    public DTOs.Article MapEntityToDto(DataAccess.Entities.Article articleEntity)
    {
        return new DTOs.Article(articleEntity.Id, articleEntity.Title, articleEntity.Content, articleEntity.CategoryId);
    }

    public void UpdateEntityWithDto(DataAccess.Entities.Article articleEntity, DTOs.Article articleDto)
    {
        articleEntity.Title = articleDto.Title;
        articleEntity.Content = articleDto.Content;
        articleEntity.CategoryId = articleDto.CategoryId;
    }
}