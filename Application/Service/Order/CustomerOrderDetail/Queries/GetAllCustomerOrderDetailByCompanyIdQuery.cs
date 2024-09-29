using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCustomerOrderDetailByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetCustomerOrderDetailResponseDTO>>;
}
    