using Application.Caching;
using Application.DTO.Response;
using Application.Interface;
using MediatR;

namespace Application.Service.Queries
{
    // this is for caching the query
    //public record GetWarehouseByIdQuery(Guid Id) : ICachedQuery<GetWarehouseResponseDTO>
    //{
    //    public string Key => $"{CacheKeys.Warehouse}-{Id}";
    //    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    //}

    public record GetWarehouseByIdQuery(Guid Id) : IRequest<GetWarehouseResponseDTO>;
}
