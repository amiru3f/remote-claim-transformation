namespace RemoteClaimTransformer.Auth;

using RemoteClaimTransformer.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

//This transformer will be triggered in the case of passing a valid JWT token to the .Net application
internal sealed class RemoteClaimsTransformation : IClaimsTransformation
{
    private readonly UserClaimService _userClaimService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RemoteClaimsTransformation(UserClaimService userClaimService, IHttpContextAccessor httpContextAccessor)
    {
        _userClaimService = userClaimService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        IEnumerable<Claim> userClaims = Enumerable.Empty<Claim>();

        if (principal?.Identity?.IsAuthenticated is true)
        {
            userClaims = await _userClaimService.GetPermissionsByJwtTokenOrSubAsync(_httpContextAccessor!.HttpContext!.User!.Identity!.Name!, _httpContextAccessor.HttpContext!.Request!.Headers!.Authorization!);
        }

        ClaimsIdentity identity = new(userClaims);
        identity.AddClaims(principal!.Claims);

        return new(identity);
    }
}