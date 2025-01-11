using MediatR;

namespace Application.Service.Queries
{
    public record GetAllVendorIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
