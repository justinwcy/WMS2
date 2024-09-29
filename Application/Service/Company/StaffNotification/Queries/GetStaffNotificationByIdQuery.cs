﻿using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffNotificationByIdQuery(Guid StaffNotificationId) : IRequest<GetStaffNotificationResponseDTO>;
}
