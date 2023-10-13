using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.Contributors.List;

public class ListContributorsHandler : IQueryHandler<ListContributorsQuery, Result<IEnumerable<ContributorDTO>>>
{
    private readonly IListContributorsQueryService _query;
    public ListContributorsHandler(IListContributorsQueryService queryService) => _query = queryService;
    
    public async Task<Result<IEnumerable<ContributorDTO>>> Handle(ListContributorsQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync();
        return Result.Success(result);
    }
}