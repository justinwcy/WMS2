using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductGroupIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    