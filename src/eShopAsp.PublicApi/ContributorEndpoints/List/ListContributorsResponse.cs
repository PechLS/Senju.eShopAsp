using eShopAsp.UseCases.Contributors;

namespace eShopAsp.PublicApi.ContributorEndpoints.List;

public class ListContributorsResponse
{
    public List<ContributorDTO> Contributors { get; set; } = new();
}