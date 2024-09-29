using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRefundOrderById(Guid Id) : IRequest<GetRefundOrderResponseDTO>;
}
    