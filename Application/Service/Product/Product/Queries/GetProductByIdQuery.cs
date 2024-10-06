using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<GetProductResponseDTO>;
}
    