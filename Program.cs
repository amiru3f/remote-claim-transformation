using RemoteClaimTransformer.Auth;
using RemoteClaimTransformer.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services
                .AddMemoryCache()
                .AddHttpContextAccessor()
                .AddTransient<UserClaimService>()
                .AddScoped<IClaimsTransformation, RemoteClaimsTransformation>()
                .AddAuthentication()
                .AddJwtBearer(options =>
                {
                    //Just to use any token in debugging environment
#if DEBUG
                    options.TokenValidationParameters.ValidateActor = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = false;
                    options.TokenValidationParameters.ValidateLifetime = false;

                    options.TokenValidationParameters.SignatureValidator = (token, validationParams) =>
                    {
                        return new JwtSecurityToken(token);
                    };
#endif
                });

builder.Services.AddControllers();

builder.Services.AddAuthorization();

var app = builder.Build();

app
    .UseRouting()
    .UseAuthentication();

app.MapGet("/grant-through-minimal-api", (ClaimsPrincipal user) => user!.Identity!.Name)
.RequireAuthorization(policy => policy.RequireClaim(Constants.PermissionClaimName, Constants.P1_Permission));

app.UseAuthorization();

#pragma warning disable ASP0014 // Rethrow to preserve stack details
app.UseEndpoints(ep => ep.MapControllers());
#pragma warning restore ASP0014

app.Run();