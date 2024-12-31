using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetStaffIdByEmailHandler(
        UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffIdByEmailQuery, Guid>
    {
        public async Task<Guid> Handle(GetStaffIdByEmailQuery request, CancellationToken cancellationToken)
        {
            var accountService = new AccountService(userManager, signInManager, roleManager, contextFactory);
            var staffId = await accountService.GetStaffIdByEmailAsync(request.StaffEmail);
                
            return staffId;
        }
    }
}
