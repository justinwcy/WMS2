using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCompanyIdsQuery() : IRequest<List<Guid>>;
}
