﻿using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateStaffRequestDTO : StaffBaseDTO
    {
        public Guid Id { get; set; }
    }
}
