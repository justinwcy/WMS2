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
    public class LoginStaffHandler(UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<LoginStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(LoginStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                var loginStaffRequestDTO = request.Model.Adapt<LoginStaffRequestDTO>();
                var response = await accountHandler.StaffLoginAsync(loginStaffRequestDTO);
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
