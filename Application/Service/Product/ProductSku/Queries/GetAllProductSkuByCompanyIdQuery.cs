using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductSkuByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetProductSkuResponseDTO>>;
}
    