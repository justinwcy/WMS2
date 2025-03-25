using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetCouriersByIdsQuery(List<Guid> CourierIds) : IRequest<List<GetCourierResponseDTO>>;
}
