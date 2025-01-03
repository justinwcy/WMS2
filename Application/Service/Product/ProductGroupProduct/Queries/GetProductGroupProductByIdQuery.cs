using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductGroupProductByIdQuery(Guid Id) : IRequest<GetProductGroupProductResponseDTO>;
}
    