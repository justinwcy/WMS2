using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductGroupProductIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    