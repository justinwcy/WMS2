using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllBinByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetBinResponseDTO>>;
}
    