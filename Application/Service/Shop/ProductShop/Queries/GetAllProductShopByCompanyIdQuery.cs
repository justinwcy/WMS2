using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductShopByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetProductShopResponseDTO>>;
}
    