namespace RemoteClaimTransformer.Auth;

using Microsoft.AspNetCore.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequirePermissionAttribute : Attribute, IAuthorizeData
{
    public string Permissions { set; get; }

    public RequirePermissionAttribute(string permissions)
    {
        Permissions = permissions;
        Policy = MustHavePermissionRequirementHandler.PolicyName;
    }

    //
    // Summary:
    //     Gets or sets the policy name that determines access to the resource.
    public string? Policy { get; set; }

    //
    // Summary:
    //     Gets or sets a comma delimited list of roles that are allowed to access the resource.
    public string? Roles { get; set; }

    //
    // Summary:
    //     Gets or sets a comma delimited list of schemes from which user information is
    //     constructed.
    public string? AuthenticationSchemes { get; set; }
}