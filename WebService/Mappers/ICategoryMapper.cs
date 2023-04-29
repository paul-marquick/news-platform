namespace NewsPlatform.WebService.Mappers;

public interface ICategoryMapper
{
    DTOs.Category MapEntityToDto(DataAccess.Entities.Category categoryEntity);
    void UpdateEntityWithDto(DataAccess.Entities.Category categoryEntity, DTOs.Category categoryDto);
}