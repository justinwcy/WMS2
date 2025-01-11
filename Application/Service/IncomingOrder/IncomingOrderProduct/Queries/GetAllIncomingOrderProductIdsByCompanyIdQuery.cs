using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllIncomingOrderProductIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
