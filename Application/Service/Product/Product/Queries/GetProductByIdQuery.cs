using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductById(Guid Id) : IRequest<GetProductResponseDTO>;
}
    