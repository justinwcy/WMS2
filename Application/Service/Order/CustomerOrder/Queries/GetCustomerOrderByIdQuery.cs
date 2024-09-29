using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerOrderById(Guid Id) : IRequest<GetCustomerOrderResponseDTO>;
}
    