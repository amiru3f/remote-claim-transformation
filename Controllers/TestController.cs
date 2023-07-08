using Microsoft.AspNetCore.Mvc;
using RemoteClaimTransformer.Auth;

namespace RemoteClaimTransformer.Controllers;

public class TestController : Controller
{
    [Route("access-through-controller")]
    [RequirePermission("P1")]
    public string Index() => $"Access granted to {User!.Identity!.Name}";
}
