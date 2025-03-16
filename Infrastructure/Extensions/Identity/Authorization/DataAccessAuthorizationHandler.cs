using System.Globalization;

using Application.Constants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extensions.Identity.Authorization
{
    // This class contains logic for determining whether DataAccessRequirements in authorization
    // policies are satisfied or not
    internal class DataAccessAuthorizationHandler(ILogger<DataAccessAuthorizationHandler> logger)
        : AuthorizationHandler<DataAccessRequirement>
    {
        // Check whether a given DataAccessRequirement is satisfied or not for a particular context
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DataAccessRequirement requirement)
        {
            // Log as a warning so that it's very clear in sample output which authorization policies 
            // (and requirements/handlers) are in use
            logger.LogWarning("Evaluating authorization requirement for " +
                              DataAccessUtilities.GetPolicyString(requirement.AccessName, requirement.DataTableName));

            // Check if claim is present
            var dataAccessClaim = context.User.FindFirst(requirement.PolicyName);
            if (dataAccessClaim != null)
            {
                // get expiry date and check if past expiry
                var expiryDateString = dataAccessClaim.Value;
                var expiryDate = DateTime.ParseExact(
                    expiryDateString, 
                    "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture);

                var dateNow = DateTime.Now;

                if (expiryDate > dateNow)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    logger.LogInformation($"Current user's {dataAccessClaim} claim has expired");
                }
            }
            else
            {
                logger.LogInformation($"Current user's {dataAccessClaim} claim is not present");

            }

            return Task.CompletedTask;
        }
    }
}
