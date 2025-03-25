using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductGroupProductsByIdsQuery(List<Guid> ProductGroupProductIds) : IRequest<List<GetProductGroupProductResponseDTO>>;
}
