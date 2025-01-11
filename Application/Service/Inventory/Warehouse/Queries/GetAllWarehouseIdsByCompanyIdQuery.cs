using MediatR;

namespace Application.Service.Queries
{
    public record GetAllWarehouseIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    