using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetIncomingOrdersByIdsQuery(List<Guid> IncomingOrderIds) : IRequest<List<GetIncomingOrderResponseDTO>>;
}
