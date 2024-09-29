using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetShopById(Guid Id) : IRequest<GetShopResponseDTO>;
}
    