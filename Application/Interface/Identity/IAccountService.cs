using Application.DTO.Request;
using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.Identity;

namespace Application.Interface.Identity
{
    public interface IAccountService
    {
        Task<ServiceResponse> StaffLoginAsync(LoginStaffRequestDTO model);
        Task<ServiceResponse> StaffLogoutAsync();
        Task<ServiceResponse> CreateStaffAsync(CreateStaffRequestDTO model);
        Task<IEnumerable<GetStaffWithClaimResponseDTO>> GetAllStaffWithClaimsAsync(Guid companyId);
        Task SetUpAsync();
        Task<ServiceResponse> UpdateStaffAsync(ChangeStaffClaimRequestDTO model);
        Task<ServiceResponse> DeleteStaffAsync(string staffEmail);
    }
}
