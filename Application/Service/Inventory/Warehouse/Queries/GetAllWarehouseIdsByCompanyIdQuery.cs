using Application.Caching;
using Application.Interface;
using MediatR;

namespace Application.Service.Queries
{
    // this is for caching the query
    //public record GetAllWarehouseIdsByCompanyIdQuery(Guid CompanyId) : ICachedQuery<List<Guid>>
    //{
    //    public string Key => $"{CacheKeys.GetAllWarehouseIdsByCompanyIdQuery}-{CompanyId}";
    //    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    //}

    public record GetAllWarehouseIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
