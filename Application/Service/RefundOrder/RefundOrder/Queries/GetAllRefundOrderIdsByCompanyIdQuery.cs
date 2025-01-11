using MediatR;

namespace Application.Service.Queries
{
    public record GetAllRefundOrderIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    