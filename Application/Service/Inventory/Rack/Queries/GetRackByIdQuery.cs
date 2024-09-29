using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRackByIdQuery(Guid Id) : IRequest<GetRackResponseDTO>;
}
    