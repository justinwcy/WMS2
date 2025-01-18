using MediatR;

namespace Application.Service.Queries
{
    public record GetAllBinIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
 