using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;
using Mapster;
using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllStaffByCompanyIdHandler(
        UserManager<WmsStaff> userManager,
        SignInManager<WmsStaff> signInManager,
        RoleManager<IdentityRole> roleManager,
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllStaffByCompanyIdQuery, IEnumerable<GetStaffResponseDTO>>
    {
        public async Task<IEnumerable<GetStaffResponseDTO>> Handle(GetAllStaffByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var accountHandler = new Account(userManager, signInManager, roleManager, contextFactory);
                var staffsWithClaimResponseDTO = await accountHandler.GetAllStaffWithClaimsAsync(request.CompanyId);

                await using var wmsDbContext = contextFactory.CreateDbContext();

                var response = new List<GetStaffResponseDTO>();
                foreach (var staffWithClaimResponseDTO in staffsWithClaimResponseDTO)
                {
                    var getStaffResponseDTO = staffWithClaimResponseDTO.Adapt<GetStaffResponseDTO>();
                    getStaffResponseDTO.CompanyId = request.CompanyId;
                    
                    response.Add(getStaffResponseDTO);
                }
                
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
