using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductGroupByIdQuery(Guid Id) : IRequest<GetProductGroupResponseDTO>;
}
    