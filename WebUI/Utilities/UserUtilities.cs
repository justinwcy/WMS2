using Application.DTO.Response;
using Application.Service.Queries;
using MediatR;

using Microsoft.AspNetCore.Components.Authorization;

namespace WebUI.Utilities
{
    public static class UserUtilities
    {
        public static async Task<GetStaffResponseDTO> GetCurrentUser(IMediator mediator,
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
                return getStaffResponseDTO;
            }

            throw new Exception("User not found!");
        }
    }
}
