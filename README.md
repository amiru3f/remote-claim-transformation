# A simple .Net Claim Authorization using  `claims-transformer`

### This app demostrates how to read user claims through a remote endpoint and authorize with dynamic permission claims without complex hackings

***

Problem: Consider you want to authorize your APIs, either `Minimal-Apis` or `Controller-Based` ones with permission-claim comming from a remote endpoint instead of being inside the JWT

There are lots of hacks to solve such a simple problem.

* Using `Authorization filters`
* Replacing .Net Authorization Service (wow :D)
* Pushing `custom-middlewares` before built-in ones
* A combination of customized attributes and reflection or code generators
* A combination of using `[Authorize(Role=..)]` with putting the permissions into the `Role` claim and hacking `User.IsInRole(...)` function by changing `RoleClaimType` to `"Permission"`
* and:

Pretty simple .Net out-of-the-box supprted interface, `IClaimsTranformation` which lets you transform the user claims before entering the Authorization middleware. This approach is supported both in MinimalApis and Controllers style. For the sake of MinimalApis there is already an implemented `RequireClaim()` extensions that enables  you to check the specific permission claim, and for the latter you just need an additional `Policy-Requirement` pair to ensure the specific permission value exists in the user claims

## How to run

Just run the project in debug mode and use the following curl to check both of the endpoints

```bash
$ curl localhost:5000/grant-through-minimal-api -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0MGE5ZGRmOS0yZmY0LTQ3NDAtOGI3Yy1iYmRjOGI2NjVkNmMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSm9obiBEb2UiLCJpYXQiOjE1MTYyMzkwMjIsImV4cCI6MjUxNjIzOTAyMiwiaXNzIjoidGVzdC10cmFuc2Zvcm1lciJ9.Cjfq2WjBlaMwIr6lXo4STrRaDLrryiAHcjJZSMBKUkE' -v
```

```bash
$ curl localhost:5000/access-through-controller -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0MGE5ZGRmOS0yZmY0LTQ3NDAtOGI3Yy1iYmRjOGI2NjVkNmMiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSm9obiBEb2UiLCJpYXQiOjE1MTYyMzkwMjIsImV4cCI6MjUxNjIzOTAyMiwiaXNzIjoidGVzdC10cmFuc2Zvcm1lciJ9.Cjfq2WjBlaMwIr6lXo4STrRaDLrryiAHcjJZSMBKUkE' -v
```

See also:

* [.Net Claims Transformation](https://source.dot.net/#Microsoft.AspNetCore.Authentication.Abstractions/IClaimsTransformation.cs)
* [Pass Through Authorization Handler](https://source.dot.net/#Microsoft.AspNetCore.Authorization/PassThroughAuthorizationHandler.cs), the Infrastructure class which allows an `AuthorizationRequirement` to  be its own `AuthorizationHandler`
* [.Net authorization service](https://source.dot.net/#Microsoft.AspNetCore.Authentication.Core/AuthenticationService.cs,92) which uses the transformer