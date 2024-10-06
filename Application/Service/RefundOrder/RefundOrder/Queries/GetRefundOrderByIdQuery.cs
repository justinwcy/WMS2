using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRefundOrderByIdQuery(Guid Id) : IRequest<GetRefundOrderResponseDTO>;
}
    