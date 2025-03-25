using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerOrderDetailsByIdsQuery(List<Guid> CustomerOrderDetailIds) : IRequest<List<GetCustomerOrderDetailResponseDTO>>;
}
