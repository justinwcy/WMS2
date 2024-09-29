using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllLocationByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetLocationResponseDTO>>;
}
    