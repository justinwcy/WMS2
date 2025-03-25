using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetBinsByIdsQuery(List<Guid> BinIds) : IRequest<List<GetBinResponseDTO>>;
}
