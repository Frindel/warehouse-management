using AutoMapper;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence.Mapping;

public class UnitsProfile : Profile
{
    public UnitsProfile()
    {
        CreateMap<UnitEntity, Unit>();
        CreateMap<Unit, UnitEntity>();
    }
}