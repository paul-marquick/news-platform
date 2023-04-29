namespace NewsPlatform.WebService.Mappers;

public interface ICategoryMapper
{
    Models.Category MapEntityToModel(DataAccess.Entities.Category categoryEntity);
    DataAccess.Entities.Category MapModelToEntity(Models.Category categoryModel);
}