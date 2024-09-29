using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllIncomingOrderByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetIncomingOrderResponseDTO>>;
}
