using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductShopById(Guid Id) : IRequest<GetProductShopResponseDTO>;
}
    