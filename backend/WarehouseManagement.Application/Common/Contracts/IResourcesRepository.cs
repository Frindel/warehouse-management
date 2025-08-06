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
    /// Проверяет, существует ли уже ресурс с данным названием
    /// </summary>
    /// <param name="name">Название ресурса</param>
    /// <returns><c>true</c> - ресурс существует; <c>false</c> - ресурс</returns>
    Task<bool> IsExist(string name);
    
    /// <summary>
    /// Проверяет, существует ли уже ресурс с данным id
    /// </summary>
    /// <param name="id">id ресурса</param>
    /// <returns><c>true</c> - ресурс существует; <c>false</c> - отсутствует</returns>
    Task<bool> IsExist(Guid id);
    
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