using eShopAsp.PublicApi;

namespace eShopAsp.PublicApi.AuthEndpoints;

public class AuthenticateResponse : BaseResponse
{
    public AuthenticateResponse(Guid correlationId) : base(correlationId){}
    public AuthenticateResponse() {}

    public bool Result { get; set; } = false;
    public string Token { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public bool IsLookedOut { get; set; } = false;
    public bool IsNotAllowed { get; set; } = false;
    public bool RequiresTwoFactors { get; set; } = false;
 }