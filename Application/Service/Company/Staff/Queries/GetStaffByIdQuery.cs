﻿using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffByIdQuery(Guid StaffId) : IRequest<GetStaffResponseDTO>;
}
