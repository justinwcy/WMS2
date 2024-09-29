using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllZoneStaffByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetZoneStaffResponseDTO>>;
}
    