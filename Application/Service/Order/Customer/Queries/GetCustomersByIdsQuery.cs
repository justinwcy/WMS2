using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomersByIdsQuery(List<Guid> CustomerIds) : IRequest<List<GetCustomerResponseDTO>>;
}
