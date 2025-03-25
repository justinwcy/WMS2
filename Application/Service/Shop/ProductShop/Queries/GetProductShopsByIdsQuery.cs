using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductShopsByIdsQuery(List<Guid> ProductShopIds) : IRequest<List<GetProductShopResponseDTO>>;
}
