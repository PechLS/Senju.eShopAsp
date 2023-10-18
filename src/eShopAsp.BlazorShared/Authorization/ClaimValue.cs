namespace eShopAsp.BlazorShared.Authorizations;

public class ClaimValue 
{
    public string Type { get; set; }
    public string Name { get; set;}

    public ClaimValue() {}
    public ClaimValue(string type, string name) 
    {
        Type = type;
        Name = name;
    }
    
}