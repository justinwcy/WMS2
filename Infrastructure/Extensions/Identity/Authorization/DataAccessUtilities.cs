using System.Globalization;
using Application.Constants;

using Microsoft.AspNetCore.Components.Authorization;

namespace Infrastructure.Extensions.Identity.Authorization
{
    public static class DataAccessUtilities
    {
        public static string GetPolicyString(string accessName, string dataTableName)
        {
            return $"{PolicyConstant.DataAccessPrefix}{accessName}.{dataTableName}";
        }

        public static async Task<bool> GetExpiryDateClaim(
            AuthenticationStateProvider authenticationStateProvider,
            string claimString)
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var policyClaim = user.Claims.FirstOrDefault(claim => claim.Type == claimString);

            var isExpired = true;
            if (policyClaim != null)
            {
                var expiryDateString = policyClaim.Value;
                var expiryDate = DateTime.ParseExact(
                    expiryDateString,
                    "yyyy/MMM/dd HH:mm:ss",
                    CultureInfo.InvariantCulture);
                var dateNow = DateTime.Now;

                if (expiryDate > dateNow)
                {
                    isExpired = false;
                }
            }

            return isExpired;
        }
    }
}
