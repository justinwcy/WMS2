using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerOrdersByIdsQuery(List<Guid> CustomerOrderIds) : IRequest<List<GetCustomerOrderResponseDTO>>;
}
