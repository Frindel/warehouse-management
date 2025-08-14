using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Common.Contracts;

public interface IReceiptsRepository
{
    /// <summary>
    ///  Возвращает документ поступления с указанным названием, если он существует
    /// </summary>
    /// <param name="number">Номер документа поступления</param>
    /// <returns>Документ поступления с ресурсами в нем</returns>
    Task<Receipt?> TryGet(string number);
    
    /// <summary>
    ///  Возвращает документ поступления с указанным id, если он существует
    /// </summary>
    /// <param name="id">Id документа поступления</param>
    /// <returns>Документ поступления с ресурсами в нем</returns>
    Task<Receipt?> TryGet(Guid id);
    
    /// <summary>
    /// Создает новый документ поступления
    /// </summary>
    /// <param name="document">Документ поступления, с ресурсами</param>
    /// <returns>Id созданного документа поступления</returns>
    Task<Guid> Create(Receipt document);

    Task Delete(Guid id);
    
    Task Update(Receipt document);

    /// <summary>
    /// Возвращает список поступлений, подходящих, под указанные фильтры
    /// </summary>
    /// <param name="number">Номер документа поступления</param>
    /// <param name="period">Временной период поиска</param>
    /// <param name="unitIds">Id единиц измерения</param>
    /// <param name="productIds">Id ресурсов</param>
    /// <returns>Документы поступления</returns>
    Task<List<Receipt>> Find(List<string>? number = null, (DateOnly begin, DateOnly end)? period = null, List<Guid>? unitIds = null, List<Guid>? productIds = null);
    
    Task<List<Receipt>> GetOnlyReceipts();
    
    Task<(DateOnly begin, DateOnly end)> GetMaxPeriod();
}