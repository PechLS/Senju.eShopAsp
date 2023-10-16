namespace eShopAsp.Core.Interfaces.Services;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string username);
}