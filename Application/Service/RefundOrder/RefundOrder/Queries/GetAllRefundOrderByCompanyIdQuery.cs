using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllRefundOrderByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetRefundOrderResponseDTO>>;
}
    