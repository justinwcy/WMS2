using Application.DTO.BaseDTO.Identity;

namespace Application.DTO.Request.Identity
{
    public class ChangeStaffClaimRequestDTO : BaseStaffClaimsDTO
    {
        public Guid Id { get; set; }
    }
}
