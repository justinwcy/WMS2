using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductGroupProductByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetProductGroupProductResponseDTO>>;
}
    