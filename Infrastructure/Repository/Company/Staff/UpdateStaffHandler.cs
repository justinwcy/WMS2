using Application.DTO.Request;
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
                    .Include(staff=>staff.Zones)
                    .FirstOrDefaultAsync(staff => staff.Id == request.Model.Id,
                        cancellationToken);
                if (staffFound == null)
                {
                    return GeneralDbResponses.ItemNotFound(request.Model.Id.ToString());
                }

                staffFound.CreatedBy = request.Model.CreatedBy;
                    
                var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
                var updateStaffRequestDTO = request.Model.Adapt<UpdateStaffRequestDTO>();
                var response = await accountHandler.UpdateStaffAsync(updateStaffRequestDTO);

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
