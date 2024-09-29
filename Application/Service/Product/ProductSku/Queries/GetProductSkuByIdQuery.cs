using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductSkuById(Guid Id) : IRequest<GetProductSkuResponseDTO>;
}
    