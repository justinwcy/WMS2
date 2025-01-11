using MediatR;

namespace Application.Service.Queries
{
    public record GetAllZoneStaffIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    