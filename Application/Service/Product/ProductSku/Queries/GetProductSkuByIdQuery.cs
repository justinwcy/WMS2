using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductSkuByIdQuery(Guid Id) : IRequest<IEnumerable<GetProductSkuResponseDTO>>;
}
    