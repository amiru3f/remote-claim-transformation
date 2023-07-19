using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using RemoteClaimTransformer.Auth;

namespace RemoteClaimTransformer.Services;

internal static partial class LoggerExtensions
{
    [LoggerMessage(0, LogLevel.Information, "going to fetch claims using {sub} or {token}", EventName = nameof(GoingToFetchClaimsForToken))]
    public static partial void GoingToFetchClaimsForToken(this ILogger logger, string sub, string token);
}
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

        _logger.GoingToFetchClaimsForToken(sub, jwtToken);
        const string permClaimName = Auth.Constants.PermissionClaimName;

        return ValueTask.FromResult<IEnumerable<Claim>>(new List<Claim>()
        {
            new(permClaimName, Constants.P1_Permission),
            new(permClaimName, Constants.P2_Permission)
        });
    }

}