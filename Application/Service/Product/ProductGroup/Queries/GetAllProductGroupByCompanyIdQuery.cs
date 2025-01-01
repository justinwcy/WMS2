using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductGroupByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetProductGroupResponseDTO>>;
}
    