using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteCompanyCommand(Guid Id) : IRequest<ServiceResponse>;
}
