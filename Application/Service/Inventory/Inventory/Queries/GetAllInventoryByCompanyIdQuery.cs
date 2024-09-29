using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllInventoryByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetInventoryResponseDTO>>;
}
    