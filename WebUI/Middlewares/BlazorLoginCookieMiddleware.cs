using System.Collections.Concurrent;
using Application.DTO.Request.Identity;
using Application.Service.Commands;
using MediatR;
using WebUI.Components.Pages.Identity.Authentication;

namespace WebUI.Middlewares
{
    public class BlazorCookieLoginMiddleware
    {
        public static IDictionary<Guid, LoginStaffRequestDTO> LoginDetails { get; private set; }
            = new ConcurrentDictionary<Guid, LoginStaffRequestDTO>();

        private readonly RequestDelegate _next;

        public BlazorCookieLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMediator mediator)
        {
            if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
            {
                var key = Guid.Parse(context.Request.Query["key"]);
                var loginStaffRequestDTO = LoginDetails[key];

                var command = new LoginStaffCommand(loginStaffRequestDTO);

                var response = await mediator.Send(command);
                loginStaffRequestDTO.Password = null;
                if (response.Success)
                {
                    LoginDetails.Remove(key);
                    context.Response.Redirect("/");
                }
                else
                {
                    context.Response.Redirect(Login.PageUrl);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
