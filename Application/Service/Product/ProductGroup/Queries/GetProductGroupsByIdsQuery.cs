using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductGroupsByIdsQuery(List<Guid> ProductGroupIds) : IRequest<List<GetProductGroupResponseDTO>>;
}
