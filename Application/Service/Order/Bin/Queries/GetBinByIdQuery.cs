using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetBinByIdQuery(Guid Id) : IRequest<GetBinResponseDTO>;
}
    