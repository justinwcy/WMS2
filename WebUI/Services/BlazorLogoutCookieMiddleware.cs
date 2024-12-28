using System.Collections.Concurrent;

using Application.DTO.Request.Identity;
using Application.Service.Commands;

using MediatR;

using WebUI.Components.Pages.Identity.Authentication;

namespace WebUI.Services
{
    public class BlazorCookieLogoutMiddleware
    {
        public static IDictionary<Guid, LogoutStaffRequestDTO> LogoutDetails { get; private set; }
            = new ConcurrentDictionary<Guid, LogoutStaffRequestDTO>();

        private readonly RequestDelegate _next;

        public BlazorCookieLogoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMediator mediator)
        {
            if (context.Request.Path == "/logout" && context.Request.Query.ContainsKey("key"))
            {
                var key = Guid.Parse(context.Request.Query["key"]);
                var logoutStaffRequestDTO = LogoutDetails[key];

                var command = new LogoutStaffCommand(logoutStaffRequestDTO);

                var response = await mediator.Send(command);
                if (response.Success)
                {
                    LogoutDetails.Remove(key);
                    context.Response.Redirect(Login.PageUrl);
                }
                else
                {
                    context.Response.Redirect("/");
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
