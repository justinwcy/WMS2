using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetShopsByIdsQuery(List<Guid> ShopIds) : IRequest<List<GetShopResponseDTO>>;
}
