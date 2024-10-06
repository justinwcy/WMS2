using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductShopByIdQuery(Guid Id) : IRequest<GetProductShopResponseDTO>;
}
    