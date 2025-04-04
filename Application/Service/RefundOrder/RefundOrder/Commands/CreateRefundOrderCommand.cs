using Application.DTO.Request;
using Application.DTO.Response;

using MediatR;

namespace Application.Service.Commands
{
    public record CreateRefundOrderCommand(CreateRefundOrderRequestDTO Model) : IRequest<CreateRefundOrderResponseDTO>;
}
    