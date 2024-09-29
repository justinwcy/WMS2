using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetZoneById(Guid Id) : IRequest<GetZoneResponseDTO>;
}
    