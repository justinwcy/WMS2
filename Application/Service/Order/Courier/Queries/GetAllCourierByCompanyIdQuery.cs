using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCourierByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetCourierResponseDTO>>;
}
    