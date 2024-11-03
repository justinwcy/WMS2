using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetProductRackByIdQuery(Guid Id) : IRequest<GetProductRackResponseDTO>;
}
    