namespace NewsPlatform.WebService.Mappers;

public class CategoryMapper : ICategoryMapper
{
    public DTOs.Category MapEntityToDto(DataAccess.Entities.Category categoryEntity)
    {
        return new DTOs.Category(categoryEntity.Id, categoryEntity.Name);
    }

    public void UpdateEntityWithDto(DataAccess.Entities.Category categoryEntity, DTOs.Category categoryDto)
    {
        categoryEntity.Name = categoryDto.Name;
    }
}