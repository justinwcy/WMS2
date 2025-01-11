using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCourierIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
    