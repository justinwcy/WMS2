using MediatR;

namespace Application.Service.Queries
{
    public record GetAllInventoryIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    