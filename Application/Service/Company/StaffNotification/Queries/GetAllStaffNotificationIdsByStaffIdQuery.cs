using MediatR;

namespace Application.Service.Queries
{
    public record GetAllStaffNotificationIdsByStaffIdQuery(Guid StaffId) : 
        IRequest<List<Guid>>;
}
