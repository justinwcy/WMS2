using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRefundOrderProductById(Guid Id) : IRequest<GetRefundOrderProductResponseDTO>;
}
    