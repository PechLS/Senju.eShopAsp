namespace eShopAsp.PublicApi.AuthEndpoints;

public class UserInfo 
{
    public static readonly UserInfo Anonymous = new();
    public bool IsAuthenticated { get; set; }
    public string NameClaimType { get; set; } = string.Empty;
    public string ValueClaimType { get; set; } = string.Empty;
    public IEnumerable<ClaimValue> claimValues = new List<ClaimValue>();
}