using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCourierByIdQuery(Guid Id) : IRequest<GetCourierResponseDTO>;
}
    