using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllIncomingOrderIdsByCompanyIdQuery(Guid CompanyId) : IRequest<List<Guid>>;
}
