using Microsoft.AspNetCore.Mvc;
using RemoteClaimTransformer.Auth;

namespace RemoteClaimTransformer.Controllers;

public class TestController : Controller
{
    [Route("access-through-controller")]
    [RequireClaim(Constants.PermissionClaimName, Constants.P1_Permission)]
    public string Index() => $"Access granted to {User!.Identity!.Name}";
}
