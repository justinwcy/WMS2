using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository
{
    public class UpdateStaffHandler(UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<UpdateStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                var changeStaffClaimRequestDTO = request.Model.Adapt<ChangeStaffClaimRequestDTO>();
                var response = await accountHandler.UpdateStaffAsync(changeStaffClaimRequestDTO);
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
