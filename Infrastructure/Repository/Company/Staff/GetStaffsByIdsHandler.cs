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
    public class GetStaffsByIdsHandler(
        UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetStaffsByIdsQuery, List<GetStaffResponseDTO>>
    {
        public async Task<List<GetStaffResponseDTO>> Handle(GetStaffsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffsFound = await wmsDbContext.Staffs.AsNoTracking()
                .Where(staff => request.StaffIds.Contains(staff.Id))
                .Include(staff => staff.Zones)
                .ToListAsync(cancellationToken);

            var accountService = new AccountService(userManager, signInManager, roleManager, contextFactory);
            var result = new List<GetStaffResponseDTO>();
            foreach (var staffFound in staffsFound)
            {
                var getStaffResponseDTO = staffFound.Adapt<GetStaffResponseDTO>();
                var userFound = await accountService.GetWmsStaffById(staffFound.Id);
                var userRoles = await accountService.GetRolesByEmailAsync(userFound.Email);
                getStaffResponseDTO.Roles = userRoles.ToList();

                var userClaims = await accountService.GetClaimsByEmailAsync(userFound.Email);
                getStaffResponseDTO.Claims = userClaims;
                getStaffResponseDTO.FirstName = userFound.FirstName;
                getStaffResponseDTO.LastName = userFound.LastName;
                getStaffResponseDTO.Email = userFound.Email;
                getStaffResponseDTO.CompanyId = staffFound.CompanyId;
                getStaffResponseDTO.CreatedBy = staffFound.CreatedBy;
                getStaffResponseDTO.ZoneIds = staffFound.Zones.Select(zone => zone.Id).ToList();

                result.Add(getStaffResponseDTO);
            }
            
            return result;
        }
    }
}
