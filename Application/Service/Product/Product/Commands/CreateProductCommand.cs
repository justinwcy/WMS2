using Application.DTO.Request;
using Application.DTO.Response;

using MediatR;

namespace Application.Service.Commands
{
    public record CreateProductCommand(CreateProductRequestDTO Model) : IRequest<CreateProductResponseDTO>;
}
    