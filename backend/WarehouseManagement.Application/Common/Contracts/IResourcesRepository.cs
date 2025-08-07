using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Common.Contracts;

public interface IResourcesRepository
{
    /// <summary>
    /// Создает новый ресурс
    /// </summary>
    /// <param name="resource">Ресурс</param>
    /// <returns>Идентификатор ресурса</returns>
    Task<Guid> Create(Resource resource);

    /// <summary>
    /// Возвращает ресурс с указанным названием, если он существует
    /// </summary>
    /// <param name="name">Id ресурса</param>
    Task<Resource?> TryGet(string name);

    /// <summary>
    /// Возвращает единицу измерения с указанным id, если она существует
    /// </summary>
    /// <param name="id">Id ресурса</param>
    Task<Resource?> TryGet(Guid id);
    
    /// <summary>
    /// Обновляет информацию о ресурсе
    /// </summary>
    /// <param name="resource">Ресурс</param>
    Task Update(Resource resource);

    /// <summary>
    /// Проверяет, используется ли ресурс
    /// </summary>
    /// <param name="id">Id ресурса</param>
    /// <returns><c>true</c> - ресурс используется; <c>false</c> - отсутствует</returns>
    Task<bool> IsUse(Guid id);

    /// <summary>
    /// Удаляет ресурс
    /// </summary>
    /// <param name="id">Id ресурса</param>
    Task Delete(Guid id);

    /// <summary>
    /// Возвращает список всех ресурсов
    /// </summary>
    /// <returns>Ресурсы</returns>
    Task<List<Resource>> GetAll();
}