using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Common.Contracts;

public interface IUnitsRepository
{

    /// <summary>
    /// Создает новую единицу измерения.
    /// </summary>
    /// <param name="unit">Единица измерения</param>
    /// <returns>Идентификатор единицы измерения</returns>
    Task<Guid> Create(Unit unit);

    /// <summary>
    /// Проверяет, существует ли уже единица измерения с данным названием
    /// </summary>
    /// <param name="name">Название единицы измерения</param>
    /// <returns><c>true</c> - единица измерения существует; <c>false</c> - отсутствует</returns>
    Task<bool> IsExist(string name);
    
    /// <summary>
    /// Проверяет, существует ли уже единица измерения с данным id
    /// </summary>
    /// <param name="id">id единицы измерения</param>
    /// <returns><c>true</c> - единица измерения существует; <c>false</c> - отсутствует</returns>
    Task<bool> IsExist(Guid id);
    
    /// <summary>
    /// Обновляет информацию о единицы измерения.
    /// </summary>
    /// <param name="unit">Единица измерения</param>
    Task Update(Unit unit);

    /// <summary>
    /// Проверяет, используется ли единица измерения
    /// </summary>
    /// <param name="id">Id единицы измерения</param>
    /// <returns><c>true</c> - единица измерения используется; <c>false</c> - отсутствует</returns>
    Task<bool> IsUse(Guid id);

    /// <summary>
    /// Удаляет единицу измерения
    /// </summary>
    /// <param name="id">Id единицы измерения</param>
    Task Delete(Guid id);

    /// <summary>
    /// Возвращает список всех единиц измерения
    /// </summary>
    /// <returns>Еденицы измерения</returns>
    Task<List<Unit>> GetAll();
}