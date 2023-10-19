namespace eShopAsp.PublicApi.ContributorEndpoints.Create;

public class CreateContributorResponse 
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CreateContributorResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
}