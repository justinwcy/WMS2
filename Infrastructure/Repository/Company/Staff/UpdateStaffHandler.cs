using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                await using (var wmsDbContext = contextFactory.CreateDbContext())
                {
                    var staffFound = await wmsDbContext.Staffs
                        .FirstOrDefaultAsync(staff => staff.Id == request.Model.Id,
                            cancellationToken);
                    if (staffFound == null)
                    {
                        return GeneralDbResponses.ItemNotFound(request.Model.Id.ToString());
                    }

                    staffFound.CompanyId = request.Model.CompanyId;
                    staffFound.CreatedBy = request.Model.CreatedBy;
                    wmsDbContext.Staffs.Update(staffFound);
                    
                    var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                    var changeStaffClaimRequestDTO = request.Model.Adapt<ChangeStaffClaimRequestDTO>();
                    var response = await accountHandler.UpdateStaffAsync(changeStaffClaimRequestDTO);

                    await wmsDbContext.SaveChangesAsync(cancellationToken);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
