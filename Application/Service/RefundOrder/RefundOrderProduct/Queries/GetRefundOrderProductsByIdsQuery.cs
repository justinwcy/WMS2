using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRefundOrderProductsByIdsQuery(List<Guid> RefundOrderProductIds) : IRequest<List<GetRefundOrderProductResponseDTO>>;
}
