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
        Task<ServiceResponse> CreateStaffAsync(CreateStaffRequestDTO model, bool isDebugAdmin);
        Task<IEnumerable<GetStaffWithClaimResponseDTO>> GetAllStaffWithClaimsAsync(Guid companyId);
        Task SetUpAsync();
        Task<ServiceResponse> UpdateStaffAsync(ChangeStaffClaimRequestDTO model);
        Task<ServiceResponse> DeleteStaffAsync(string staffEmail);
        Task<Guid> GetStaffIdByEmailAsync(string staffEmail);
        Task<Dictionary<string, string>> GetClaimsByEmailAsync(string staffEmail);
        Task<IEnumerable<string>> GetRolesByEmailAsync(string staffEmail);
        Task<ServiceResponse> ChangeStaffPasswordAsync(ChangePasswordStaffRequestDTO model);
    }
}
