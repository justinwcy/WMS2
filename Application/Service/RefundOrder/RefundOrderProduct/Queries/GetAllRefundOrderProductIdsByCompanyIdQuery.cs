using MediatR;

namespace Application.Service.Queries
{
    public record GetAllRefundOrderProductIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    