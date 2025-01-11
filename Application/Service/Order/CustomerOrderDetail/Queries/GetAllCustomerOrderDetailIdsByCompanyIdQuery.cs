using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCustomerOrderDetailIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    