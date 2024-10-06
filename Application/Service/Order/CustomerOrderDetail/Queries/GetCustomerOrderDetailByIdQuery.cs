using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerOrderDetailByIdQuery(Guid Id) : IRequest<GetCustomerOrderDetailResponseDTO>;
}
    