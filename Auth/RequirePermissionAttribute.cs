namespace RemoteClaimTransformer.Auth;

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class RequireClaimAttribute : Attribute, IAuthorizationRequirementData
{
    public string AllowedClaimType { get; }
    public string AllowedValues { set; get; }

    public RequireClaimAttribute(string allowedClaimType, string allowedValues)
    {
        AllowedClaimType = allowedClaimType;
        AllowedValues = allowedValues;
    }

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return new ClaimsAuthorizationRequirement(AllowedClaimType, AllowedValues?.Split(','));
    }
}