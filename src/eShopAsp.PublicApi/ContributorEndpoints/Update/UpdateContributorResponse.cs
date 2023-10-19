using eShopAsp.UseCases.Contributors;

namespace eShopAsp.PublicApi.ContributorEndpoints.Update;

public class UpdateContributorResponse
{
    public ContributorDTO Contributor { get; set; }
    public UpdateContributorResponse(ContributorDTO contributor) => Contributor = contributor;
}