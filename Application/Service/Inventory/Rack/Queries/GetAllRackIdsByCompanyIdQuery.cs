using MediatR;

namespace Application.Service.Queries
{
    public record GetAllRackIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    