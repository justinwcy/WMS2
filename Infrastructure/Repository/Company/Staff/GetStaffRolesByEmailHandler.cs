using Application.Service.Queries;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository
{
    public class GetStaffRolesByEmailHandler(
        UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffRolesByEmailQuery, IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Handle(GetStaffRolesByEmailQuery request, CancellationToken cancellationToken)
        {
            var accountService = new AccountService(userManager, signInManager, roleManager, contextFactory);
            var roles = await accountService.GetRolesByEmailAsync(request.StaffEmail);
            
            return roles;
        }
    }
}
