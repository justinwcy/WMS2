using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetWarehouseByIdQuery(Guid Id) : IRequest<GetWarehouseResponseDTO>;
}
    