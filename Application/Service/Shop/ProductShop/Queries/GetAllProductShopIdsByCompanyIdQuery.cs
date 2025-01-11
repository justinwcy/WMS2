using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductShopIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    