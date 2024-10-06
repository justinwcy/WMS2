using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<GetCustomerResponseDTO>;
}
    