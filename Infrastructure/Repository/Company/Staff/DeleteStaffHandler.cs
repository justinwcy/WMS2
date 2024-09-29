using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository
{
    public class DeleteStaffHandler(UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountHandler = new Account(userManager, signInManager, roleManager, contextFactory);
                var response = await accountHandler.DeleteStaffAsync(request.staffId);
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
