namespace NewsPlatform.WebService.Mappers;

public interface IArticleMapper
{
    DTOs.Article MapEntityToDto(DataAccess.Entities.Article articleEntity);
    void UpdateEntityWithDto(DataAccess.Entities.Article articleEntity, DTOs.Article articleDto);
}