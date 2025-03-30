using Application.DTO.Request;
using Application.DTO.Response;

using MediatR;

namespace Application.Service.Commands
{
    public record CreateCustomerOrderFromCsvCommand(CreateCustomerOrderFromCsvRequestDTO Model) : IRequest<List<CreateCustomerOrderResponseDTO>>;
}
