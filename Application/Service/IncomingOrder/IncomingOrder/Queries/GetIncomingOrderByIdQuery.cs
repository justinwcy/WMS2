using Application.DTO.Response;

using MediatR;

namespace Application.Service.Queries
{
    public record GetIncomingOrderByIdQuery(Guid Id) : IRequest<GetIncomingOrderResponseDTO>;
}
