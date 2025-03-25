using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRefundOrdersByIdsQuery(List<Guid> RefundOrderIds) : IRequest<List<GetRefundOrderResponseDTO>>;
}
