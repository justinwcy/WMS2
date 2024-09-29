using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllZoneByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetZoneResponseDTO>>;
}
    