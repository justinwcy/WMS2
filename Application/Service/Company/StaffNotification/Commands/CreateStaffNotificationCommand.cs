﻿using Application.DTO.Request;
using Application.DTO.Response;

using MediatR;

namespace Application.Service.Commands
{
    public record CreateStaffNotificationCommand(CreateStaffNotificationRequestDTO Model) : IRequest<ServiceResponse>;
}