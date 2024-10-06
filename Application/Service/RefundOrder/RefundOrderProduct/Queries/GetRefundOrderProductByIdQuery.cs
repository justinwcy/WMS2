using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRefundOrderProductByIdQuery(Guid Id) : IRequest<GetRefundOrderProductResponseDTO>;
}
    