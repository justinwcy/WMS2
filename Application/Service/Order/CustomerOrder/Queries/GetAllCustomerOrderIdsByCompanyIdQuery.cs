using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCustomerOrderIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    