using Infrastructure.Extensions.Identity;
using Microsoft.AspNetCore.Identity;
using WebUI.Services;

namespace WebUI.Services;

internal sealed class IdentityUserAccessor(
    UserManager<WmsStaff> userManager,
    IdentityRedirectManager redirectManager)
{
    public async Task<WmsStaff> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User).ConfigureAwait(false);

        if (user is null)
            redirectManager.RedirectToWithStatus("/pages/authentication/InvalidUser",
                $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);

        return user;
    }
}