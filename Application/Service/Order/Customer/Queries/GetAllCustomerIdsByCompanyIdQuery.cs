using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCustomerIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    