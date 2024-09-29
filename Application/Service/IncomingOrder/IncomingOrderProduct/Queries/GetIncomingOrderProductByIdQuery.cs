using Application.DTO.Response;

using MediatR;

namespace Application.Service.Queries
{
    public record GetIncomingOrderProductByIdQuery(Guid Id) : IRequest<GetIncomingOrderProductResponseDTO>;
}
