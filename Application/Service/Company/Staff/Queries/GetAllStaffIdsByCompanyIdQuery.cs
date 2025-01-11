using MediatR;

namespace Application.Service.Queries
{
    public record GetAllStaffIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
