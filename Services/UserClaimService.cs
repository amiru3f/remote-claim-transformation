using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;

namespace RemoteClaimTransformer.Services;

internal sealed class UserClaimService
{
    private readonly ILogger<UserClaimService> _logger;
    public UserClaimService(IMemoryCache memoryCache, ILogger<UserClaimService> logger)
    {
        //Just to cache the permissions of the user after fetching
        _ = memoryCache;
        _logger = logger;
    }

    //to fetch user claims from DB or maybe through a service call (Rest/GRPC)
    public ValueTask<IEnumerable<Claim>> GetPermissionsByJwtTokenOrSubAsync(string sub, string jwtToken)
    {
        _ = sub;
        _logger.LogTrace("going to fetch user claims for token: {token}", jwtToken);
        const string permClaimName = Auth.Constants.PermissionClaimName;
        return ValueTask.FromResult<IEnumerable<Claim>>(new List<Claim>() { new(permClaimName, "P1"), new(permClaimName, "P2") });
    }
}