using AutoMapper;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence.Mapping;

public class ReceiptDocumentsProfile : Profile
{
    public ReceiptDocumentsProfile()
    {
        CreateMap<ReceiptDocumentEntity, Receipt>();
        CreateMap<Receipt, ReceiptDocumentEntity>();
    }
}