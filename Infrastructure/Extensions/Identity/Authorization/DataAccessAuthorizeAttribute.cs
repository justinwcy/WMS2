using Application.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Extensions.Identity.Authorization
{
    // This attribute derives from the [Authorize] attribute, adding 
    // the ability for a user to specify an 'age' paratmer. Since authorization
    // policies are looked up from the policy provider only by string, this
    // authorization attribute creates is policy name based on a constant prefix
    // and the user-supplied age parameter. A custom authorization policy provider
    // (`DataAccessPolicyProvider`) can then produce an authorization policy with 
    // the necessary requirements based on this policy name.
    internal class DataAccessAuthorizeAttribute : AuthorizeAttribute
    {
        public DataAccessAuthorizeAttribute(string dataTableName, string accessName)
        {
            DataTableName = dataTableName;
            AccessName = accessName;
        }

        // Get or set the dataTableName and accessName property by manipulating the underlying Policy property
        public string DataTableName
        {
            get
            {
                var dataAccessString = Policy.Substring(PolicyConstant.DataAccessPrefix.Length);
                var splitString = dataAccessString.Split('.');
                return splitString[1];
            }
            set => Policy = DataAccessUtilities.GetPolicyString(AccessName, value);
        }

        public string AccessName
        {
            get
            {
                var dataAccessString = Policy.Substring(PolicyConstant.DataAccessPrefix.Length);
                var splitString = dataAccessString.Split('.');
                return splitString[0];
            }
            set => Policy = DataAccessUtilities.GetPolicyString(value, DataTableName);
        }
    }
}