﻿using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetStaffResponseDTO : StaffBaseDTO
    {
        public Guid Id { get; set; }
    }
}
