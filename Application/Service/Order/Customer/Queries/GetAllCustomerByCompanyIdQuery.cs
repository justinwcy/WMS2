using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCustomerByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetCustomerResponseDTO>>;
}
    