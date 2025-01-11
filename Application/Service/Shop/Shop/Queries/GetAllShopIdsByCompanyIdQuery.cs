using MediatR;

namespace Application.Service.Queries
{
    public record GetAllShopIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    