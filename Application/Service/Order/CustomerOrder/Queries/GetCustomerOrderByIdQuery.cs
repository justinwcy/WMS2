using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerOrderByIdQuery(Guid Id) : IRequest<GetCustomerOrderResponseDTO>;
}
    