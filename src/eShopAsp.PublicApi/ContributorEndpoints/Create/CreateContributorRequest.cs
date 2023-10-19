using System.ComponentModel.DataAnnotations;

namespace eShopAsp.PublicApi.ContributorEndpoints.Create;

public class CreateContributorRequest 
{
    public const string Route = "/Contributors";
    [Required] public string? Name { get; set; }
}