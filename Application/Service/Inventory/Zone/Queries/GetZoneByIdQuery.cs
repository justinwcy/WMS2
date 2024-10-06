using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetZoneByIdQuery(Guid Id) : IRequest<GetZoneResponseDTO>;
}
    