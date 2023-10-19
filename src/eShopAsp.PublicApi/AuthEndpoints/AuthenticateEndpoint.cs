using eShopAsp.Core.Interfaces.Services;
using eShopAsp.Infrastructure.Identity;
using eShopAsp.PublicApi.FluentGenerics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace eShopAsp.PublicApi.AuthEndpoints;

public class AuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<AuthenticateRequest>
    .WithActionResult<AuthenticateResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;

    public AuthenticateEndpoint(SignInManager<ApplicationUser> signInManager, ITokenClaimsService tokenClaimsService)
    {
        _signInManager = signInManager;
        _tokenClaimsService = tokenClaimsService;
    }


    [HttpPost("api/authenticate")]
    [SwaggerOperation(
        Summary = "Authenticates a user",
        Description = "Authenticates a user",
        OperationId = "auth.authenticate",
        Tags = new[] {"AuthEndpoints"}
    )]
    public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(
        AuthenticateRequest request, 
        CancellationToken cancellationToken = default)
    {
        var response = new AuthenticateResponse(request.CorrelationId());
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
        response.Result = result.Succeeded;
        response.IsLookedOut = result.IsLockedOut;
        response.IsNotAllowed = result.IsNotAllowed;
        response.RequiresTwoFactors = result.RequiresTwoFactor;
        response.UserName = request.UserName;

        if (result.Succeeded)
            response.Token = await _tokenClaimsService.GetTokenAsync(request.UserName);

        return response;
    }

}