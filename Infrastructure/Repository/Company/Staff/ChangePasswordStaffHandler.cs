using Application.DTO.Response;
using Application.Service.Commands;
using Application.Service.Queries;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository
{
    public class ChangePasswordStaffHandler(
        UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<ChangePasswordStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(ChangePasswordStaffCommand request, CancellationToken cancellationToken)
        {
            var accountService = new AccountService(userManager, signInManager, roleManager, contextFactory);
            var response = await accountService.ChangeStaffPasswordAsync(request.Model);
            
            return response;
        }
    }
}
