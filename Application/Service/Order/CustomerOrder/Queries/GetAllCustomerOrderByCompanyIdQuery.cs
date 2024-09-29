using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCustomerOrderByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetCustomerOrderResponseDTO>>;
}
    