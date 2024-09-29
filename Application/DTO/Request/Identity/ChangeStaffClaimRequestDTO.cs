using Application.DTO.BaseDTO.Identity;

namespace Application.DTO.Request.Identity
{
    public class ChangeStaffClaimRequestDTO : BaseStaffClaimsDTO
    {
        public string StaffId { get; set; }
    }
}
