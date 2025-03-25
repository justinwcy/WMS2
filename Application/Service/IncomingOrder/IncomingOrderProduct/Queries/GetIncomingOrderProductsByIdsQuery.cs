using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetIncomingOrderProductsByIdsQuery(List<Guid> IncomingOrderProductIds) : IRequest<List<GetIncomingOrderProductResponseDTO>>;
}
