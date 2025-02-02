using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCompanyHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCompanyCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var companyFound = await wmsDbContext.Companies
                    .Include(company=>company.Staffs)
                    .Include(company=>company.Warehouses)
                    .FirstOrDefaultAsync(company => company.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (companyFound == null)
                {
                    return GeneralDbResponses.ItemNotFound(request.Model.Name);
                }

                companyFound.Name = request.Model.Name;
                var staffsToAdd = await wmsDbContext.Staffs
                    .Where(staff => request.Model.StaffIds.Contains(staff.Id))
                    .ToListAsync(cancellationToken);
                companyFound.Staffs.RemoveAll(r => !request.Model.StaffIds.Contains(r.Id));
                foreach (var staffToAdd in staffsToAdd)
                {
                    if (companyFound.Staffs.All(staff => staff.Id != staffToAdd.Id))
                    {
                        companyFound.Staffs.Add(staffToAdd);
                    }
                }

                var warehousesToAdd = await wmsDbContext.Warehouses
                    .Where(warehouse => request.Model.WarehouseIds.Contains(warehouse.Id))
                    .ToListAsync(cancellationToken);
                companyFound.Warehouses.RemoveAll(warehouse => !request.Model.WarehouseIds.Contains(warehouse.Id));
                foreach (var warehouseToAdd in warehousesToAdd)
                {
                    if (companyFound.Warehouses.All(warehouse => warehouse.Id != warehouseToAdd.Id))
                    {
                        companyFound.Warehouses.Add(warehouseToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated(request.Model.Name);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
