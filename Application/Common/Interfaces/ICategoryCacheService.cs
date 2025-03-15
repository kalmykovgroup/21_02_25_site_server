namespace Application.Common.Interfaces;

public interface ICategoryCacheService
{
    Task<List<Guid>>
        GetCategoryIdsAndSubcategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default);
}