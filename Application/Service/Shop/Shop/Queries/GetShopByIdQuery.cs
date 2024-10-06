using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetShopByIdQuery(Guid Id) : IRequest<GetShopResponseDTO>;
}
    