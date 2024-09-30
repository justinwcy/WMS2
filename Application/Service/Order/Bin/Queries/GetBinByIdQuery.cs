using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetBinById(Guid Id) : IRequest<GetBinResponseDTO>;
}
    