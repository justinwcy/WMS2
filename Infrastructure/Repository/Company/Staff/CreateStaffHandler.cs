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
    public class CreateStaffHandler(UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateStaffCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                var createStaffAccountRequestDTO = request.Model.Adapt<CreateStaffAccountRequestDTO>();
                var response = await accountHandler.CreateStaffAsync(createStaffAccountRequestDTO);
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
