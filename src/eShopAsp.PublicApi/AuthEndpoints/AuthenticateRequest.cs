namespace eShopAsp.PublicApi.AuthEndpoints;

public class AuthenticateRequest : BaseRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}