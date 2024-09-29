using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.Identity;
using Application.Interface.Identity;

namespace Application.Service.Identity
{
    public class StaffAccountService : IStaffAccountService
    {
        private readonly IAccount _account;
        public StaffAccountService(IAccount account)
        {
            _account = account;
        }

        public async Task<ServiceResponse> StaffLoginAsync(LoginStaffRequestDTO model)
        {
            return await _account.StaffLoginAsync(model);
        }

        public async Task<ServiceResponse> StaffLogoutAsync()
        {
            return await _account.StaffLogoutAsync();
        }

        public async Task<ServiceResponse> CreateStaffAsync(CreateStaffAccountRequestDTO model)
        {
            return await _account.CreateStaffAsync(model);
        }

        public async Task<IEnumerable<GetStaffWithClaimResponseDTO>> GetAllStaffWithClaimsAsync(Guid companyId)
        {
            return await _account.GetAllStaffWithClaimsAsync(companyId);
        }

        public async Task SetUpAsync()
        {
            await _account.SetUpAsync();
        }

        public async Task<ServiceResponse> UpdateStaffAsync(ChangeStaffClaimRequestDTO model)
        {
            return await _account.UpdateStaffAsync(model);
        }

        public async Task<ServiceResponse> DeleteStaffAsync(string staffEmail)
        {
            return await _account.DeleteStaffAsync(staffEmail);
        }
    }
}
