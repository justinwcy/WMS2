using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetInventoryByIdQuery(Guid Id) : IRequest<GetInventoryResponseDTO>;
}
    