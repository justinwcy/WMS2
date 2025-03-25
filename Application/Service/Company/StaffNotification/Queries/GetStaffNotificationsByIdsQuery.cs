using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffNotificationsByIdsQuery(List<Guid> StaffNotificationIds) : IRequest<List<GetStaffNotificationResponseDTO>>;
}
