using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    