using eShopAsp.UseCases.Contributors;
using eShopAsp.UseCases.Contributors.List;

namespace eShopAsp.Infrastructure.Data.Queries;

public class FakeListContributorsQueryService : IListContributorsQueryService
{
    public Task<IEnumerable<ContributorDTO>> ListAsync()
    {
        var result = new List<ContributorDTO>() { new ContributorDTO(1, "Tom"), new ContributorDTO(2, "Jerry") };
        return Task.FromResult(result.AsEnumerable());
    }
}