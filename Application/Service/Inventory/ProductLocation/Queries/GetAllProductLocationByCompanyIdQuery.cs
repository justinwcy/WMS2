using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductLocationByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetProductLocationResponseDTO>>;
}
    