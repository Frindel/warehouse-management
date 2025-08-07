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
    /// Возвращает единицу измерения с указанным названием, если она существует
    /// </summary>
    /// <param name="name">Название единицы измерения</param>
    Task<Unit?> TryGet(string name);

    /// <summary>
    /// Возвращает единицу измерения с указанным id, если она существует
    /// </summary>
    /// <param name="id">Id единицы измерения</param>
    Task<Unit?> TryGet(Guid id);

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
    /// Возвращает список единиц измерения. В случае, если не указанны id будут возвращены все единицы измерения.
    /// <param name="ids">Id выбираемых единиц измерения</param>
    /// </summary>
    /// <returns>Единицы измерения</returns>
    Task<List<Unit>> Find(List<Guid>? ids = null);
}