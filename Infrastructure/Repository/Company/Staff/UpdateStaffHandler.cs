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
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var staffFound = await wmsDbContext.Staffs
                    .Include(staff=>staff.Company)
                    .Include(staff=>staff.StaffNotifications)
                    .Include(staff=>staff.Zones)
                    .FirstOrDefaultAsync(staff => staff.Id == request.Model.Id,
                        cancellationToken);
                if (staffFound == null)
                {
                    return GeneralDbResponses.ItemNotFound(request.Model.Id.ToString());
                }

                staffFound.CreatedBy = request.Model.CreatedBy;
                    
                var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                var changeStaffClaimRequestDTO = request.Model.Adapt<ChangeStaffClaimRequestDTO>();
                var response = await accountHandler.UpdateStaffAsync(changeStaffClaimRequestDTO);

                staffFound.CompanyId = request.Model.CompanyId;
                var companyFound = await wmsDbContext.Companies
                    .FirstOrDefaultAsync(company => company.Id == request.Model.CompanyId, cancellationToken);
                if (companyFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Company");
                }
                staffFound.Company = companyFound;

                var zonesToAdd = await wmsDbContext.Zones
                    .Where(zone => request.Model.ZoneIds.Contains(zone.Id))
                    .ToListAsync(cancellationToken);
                staffFound.Zones?.RemoveAll(zone => !request.Model.ZoneIds.Contains(zone.Id));
                foreach (var zoneToAdd in zonesToAdd)
                {
                    if ((bool) staffFound.Zones?.All(zone => zone.Id != zoneToAdd.Id))
                    {
                        staffFound.Zones.Add(zoneToAdd);
                    }
                }

                var staffNotificationsToAdd = await wmsDbContext.StaffNotifications
                    .Where(staffNotification => request.Model.StaffNotificationIds.Contains(staffNotification.Id))
                    .ToListAsync(cancellationToken);
                staffFound.StaffNotifications.RemoveAll(staffNotification => !request.Model.StaffNotificationIds.Contains(staffNotification.Id));
                foreach (var staffNotificationToAdd in staffNotificationsToAdd)
                {
                    if (staffFound.StaffNotifications.All(staffNotification => staffNotification.Id != staffNotificationToAdd.Id))
                    {
                        staffFound.StaffNotifications.Add(staffNotificationToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
