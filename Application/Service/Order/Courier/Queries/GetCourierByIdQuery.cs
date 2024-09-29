using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCourierById(Guid Id) : IRequest<GetCourierResponseDTO>;
}
    