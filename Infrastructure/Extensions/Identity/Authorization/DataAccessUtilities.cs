using Application.Constants;

namespace Infrastructure.Extensions.Identity.Authorization
{
    public static class DataAccessUtilities
    {
        public static string GetPolicyString(string accessName, string policyName)
        {
            return $"{PolicyConstant.DataAccessPrefix}{accessName}.{policyName}";
        }
    }
}
