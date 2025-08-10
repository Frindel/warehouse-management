using AutoMapper;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence.Mapping;

public class ReceiptResourcesProfile : Profile
{
    public ReceiptResourcesProfile()
    {
        CreateMap<ReceiptResourceEntity, ReceiptResource>();


        CreateMap<ReceiptResource, ReceiptResourceEntity>()
            .ForMember(rre => rre.UnitId, opt => opt.MapFrom(rr => rr.Unit.Id))
            .ForMember(rre => rre.ResourceId, opt => opt.MapFrom(rr => rr.Resource.Id));
    }
}