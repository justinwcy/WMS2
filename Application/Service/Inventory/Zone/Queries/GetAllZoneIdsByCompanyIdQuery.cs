using MediatR;

namespace Application.Service.Queries
{
    public record GetAllZoneIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    