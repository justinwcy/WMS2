using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerOrderDetailById(Guid Id) : IRequest<GetCustomerOrderDetailResponseDTO>;
}
    