namespace RemoteClaimTransformer.Auth;

using Microsoft.AspNetCore.Authorization;

internal sealed class MustHavePermissionRequirementHandler : IAuthorizationRequirement, IAuthorizationHandler
{
    internal const string PolicyName = "RequiresPermission";

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            var authDatum = endpoint?.Metadata.GetOrderedMetadata<RequirePermissionAttribute>() ?? Array.Empty<RequirePermissionAttribute>();
            var permissionsString = authDatum.Select(x => x.Permissions).FirstOrDefault();
            var requiredPermissions = permissionsString?.Split(",").Select(x => x.Trim()) ?? Array.Empty<string>();

            foreach (var requiredPermission in requiredPermissions)
            {
                if (!context.User.HasClaim(Constants.PermissionClaimName, requiredPermission))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
            }

            context.Succeed(this);
        }

        return Task.CompletedTask;
    }
}