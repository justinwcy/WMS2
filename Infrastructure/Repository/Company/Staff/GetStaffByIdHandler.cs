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
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffFound = await wmsDbContext.Staffs.AsNoTracking()
                .Include(staff => staff.Zones)
                .Include(staff => staff.StaffNotifications)
                .FirstAsync(staff => staff.Id == request.StaffId, cancellationToken);

            var accountService = new AccountService(userManager, signInManager, roleManager, contextFactory);
            var userFound = await accountService.GetWmsStaffById(request.StaffId);

            var userRoles = await accountService.GetRolesByEmailAsync(userFound.Email);
            var getStaffResponseDTO = userFound.Adapt<GetStaffResponseDTO>();
            getStaffResponseDTO.Roles = userRoles.ToList();

            getStaffResponseDTO.CompanyId = staffFound.CompanyId;
            getStaffResponseDTO.CreatedBy = staffFound.CreatedBy;
            getStaffResponseDTO.ZoneIds = staffFound.Zones.Select(zone => zone.Id).ToList();
            getStaffResponseDTO.StaffNotificationIds = staffFound
                .StaffNotifications
                .Select(staffNotification => staffNotification.Id)
                .ToList();
            return getStaffResponseDTO;
        }
    }
}
