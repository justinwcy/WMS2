using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllRefundOrderProductByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetRefundOrderProductResponseDTO>>;
}
    