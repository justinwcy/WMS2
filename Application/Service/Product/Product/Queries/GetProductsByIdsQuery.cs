using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductsByIdsQuery(List<Guid> ProductIds) : IRequest<List<GetProductResponseDTO>>;
}
