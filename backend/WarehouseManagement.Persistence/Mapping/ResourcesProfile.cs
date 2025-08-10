using AutoMapper;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence.Mapping;

public class ResourcesProfile : Profile
{
    public ResourcesProfile()
    {
        CreateMap<ResourceEntity, Resource>();
        CreateMap<Resource, ResourceEntity>();
    }
}