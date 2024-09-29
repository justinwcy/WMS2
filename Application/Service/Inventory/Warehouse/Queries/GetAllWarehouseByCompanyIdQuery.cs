using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllWarehouseByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetWarehouseResponseDTO>>;
}
    