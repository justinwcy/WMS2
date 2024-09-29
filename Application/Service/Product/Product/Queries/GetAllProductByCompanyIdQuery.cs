using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetProductResponseDTO>>;
}
    