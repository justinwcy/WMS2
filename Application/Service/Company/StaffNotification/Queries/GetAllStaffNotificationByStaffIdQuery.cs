using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllStaffNotificationByStaffIdQuery(Guid StaffId) : 
        IRequest<IEnumerable<GetStaffNotificationResponseDTO>>;
}
