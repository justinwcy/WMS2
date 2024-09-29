using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllRackByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetRackResponseDTO>>;
}
    