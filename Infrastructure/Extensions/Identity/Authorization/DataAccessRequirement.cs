using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Extensions.Identity.Authorization
{
    internal class DataAccessRequirement(string accessName, string dataTableName) : IAuthorizationRequirement
    {
        public string AccessName { get; private set; } = accessName;
        public string DataTableName { get; private set; } = dataTableName;
        public string PolicyName => DataAccessUtilities.GetPolicyString(AccessName, DataTableName);
    }
}