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
    public class GetStaffByIdHandler(
        UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffByIdQuery, GetStaffResponseDTO>
    {
        public async Task<GetStaffResponseDTO> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                var staffWithClaimResponseDTO = await accountHandler.GetStaffWithClaimsByIdAsync(request.staffId);
                if (staffWithClaimResponseDTO == null)
                {
                    return null;
                }

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var staffFound = await wmsDbContext.Staffs.AsNoTracking()
                    .FirstAsync(staff => staff.Id == request.staffId, cancellationToken);

                var getStaffResponseDTO = staffWithClaimResponseDTO.Adapt<GetStaffResponseDTO>();
                getStaffResponseDTO.CompanyId = staffFound.CompanyId;
                
                return getStaffResponseDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
