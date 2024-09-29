using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllIncomingOrderProductByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetIncomingOrderProductResponseDTO>>;
}
