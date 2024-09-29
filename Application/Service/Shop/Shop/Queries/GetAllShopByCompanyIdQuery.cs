using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllShopByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetShopResponseDTO>>;
}
    