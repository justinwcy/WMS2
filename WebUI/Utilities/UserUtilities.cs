using Application.DTO.Response;
using Application.Service.Queries;
using MediatR;

using Microsoft.AspNetCore.Components.Authorization;
using WebUI.Components.Models;

namespace WebUI.Utilities
{
    public static class UserUtilities
    {
        public static async Task<CurrentUserModel> GetCurrentUser(IMediator mediator,
            AuthenticationStateProvider authenticationStateProvider)
        {
            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var userIdentity = authenticationState.User.Identity;

            if (userIdentity != null && userIdentity.Name != null)
            {
                var getStaffIdByEmailQuery = new GetStaffIdByEmailQuery(userIdentity.Name);
                var staffId = await mediator.Send(getStaffIdByEmailQuery);
                var getStaffByIdQuery = new GetStaffByIdQuery(staffId);
                var getStaffResponseDTO = await mediator.Send(getStaffByIdQuery);

                if (getStaffResponseDTO.CompanyId == null)
                {
                    throw new Exception("Current User Must belong to a company");
                }

                var currentUserModel = new CurrentUserModel()
                {
                    Id = getStaffResponseDTO.Id,
                    CompanyId = getStaffResponseDTO.CompanyId.Value,
                };

                return currentUserModel;
            }

            throw new Exception("User not found!");
        }
    }
}
