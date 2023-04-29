namespace NewsPlatform.WebService.Mappers;

public class CategoryMapper : ICategoryMapper
{
    public Models.Category MapEntityToModel(DataAccess.Entities.Category categoryEntity)
    {
        return new Models.Category(categoryEntity.Id, categoryEntity.Name);
    }

    public DataAccess.Entities.Category MapModelToEntity(Models.Category categoryModel)
    {
        return new DataAccess.Entities.Category
        {             
            Name = categoryModel.Name 
        };
    }
}