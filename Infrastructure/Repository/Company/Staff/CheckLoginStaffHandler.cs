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
    public class CheckLoginStaffHandler(UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CheckLoginStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CheckLoginStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                var checkLoginStaffRequestDTO = request.Model.Adapt<CheckLoginStaffRequestDTO>();
                var response = await accountHandler.CheckStaffLoginAsync(checkLoginStaffRequestDTO);
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
