using Microsoft.AspNetCore.Mvc;
using RemoteClaimTransformer.Auth;
using System.Text;
using System.Globalization;

namespace RemoteClaimTransformer.Controllers;

public class TestController : Controller
{
    private static readonly CompositeFormat _format;

    static TestController()
    {
        _format = CompositeFormat.Parse("Access granted to {0}");
    }

    [Route("access-through-controller")]
    [RequireClaim(Constants.PermissionClaimName, Constants.P1_Permission)]
    public string Index() => string.Format<string>(CultureInfo.InvariantCulture, _format, User!.Identity!.Name!);
    //This generic is not required here -----^----- put it there for the sake of readability. Hit F12 to see the implementation
}
