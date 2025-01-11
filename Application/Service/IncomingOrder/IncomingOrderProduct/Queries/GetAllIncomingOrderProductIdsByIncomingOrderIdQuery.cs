using MediatR;

namespace Application.Service.Queries
{
    public record GetAllIncomingOrderProductIdsByIncomingOrderIdQuery(Guid IncomingOrderId) : IRequest<List<Guid>>;
}
