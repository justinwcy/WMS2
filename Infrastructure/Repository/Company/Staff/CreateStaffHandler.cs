using Application.DTO.Request;
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
        IRequestHandler<CreateStaffCommand, CreateStaffResponseDTO>
    {
        public async Task<CreateStaffResponseDTO> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            var accountHandler = new AccountService(userManager, signInManager, roleManager, contextFactory);
            var createStaffRequestDTO = request.Model.Adapt<CreateStaffRequestDTO>();
            var staffId = await accountHandler.CreateStaffAsync(createStaffRequestDTO);
            return new CreateStaffResponseDTO(){ Id = staffId };
        }
    }
}
