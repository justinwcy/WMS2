using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetLocationByIdQuery(Guid Id) : IRequest<GetLocationResponseDTO>;
}
    