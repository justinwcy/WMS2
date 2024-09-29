using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllStaffByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetStaffResponseDTO>>;
}
