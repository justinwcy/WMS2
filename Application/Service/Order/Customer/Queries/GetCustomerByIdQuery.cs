using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerById(Guid Id) : IRequest<GetCustomerResponseDTO>;
}
    