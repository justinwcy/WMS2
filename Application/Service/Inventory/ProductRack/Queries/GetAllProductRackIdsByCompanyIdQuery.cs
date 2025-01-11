using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductRackIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    