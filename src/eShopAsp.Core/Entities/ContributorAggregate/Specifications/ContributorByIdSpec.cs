using eShopAsp.Core.Extensions;

namespace eShopAsp.Core.Entities.ContributorAggregate.Specifications;

public sealed class ContributorByIdSpec : Specification<Contributor>
{
    public ContributorByIdSpec(int contributorId)
    {
        Query.Where(contributor => contributor.Id == contributorId);
    }
}