using Application.Service.Queries;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository
{
    public class GetStaffClaimsByEmailHandler(
        UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffClaimsByEmailQuery, Dictionary<string, string>>
    {
        public async Task<Dictionary<string, string>> Handle(GetStaffClaimsByEmailQuery request, CancellationToken cancellationToken)
        {
            var accountService = new AccountService(userManager, signInManager, roleManager, contextFactory);
            var roles = await accountService.GetClaimsByEmailAsync(request.StaffEmail);
            
            return roles;
        }
    }
}
