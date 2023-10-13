using eShopAsp.Core.Entities.ContributorAggregate;
using eShopAsp.Core.Entities.ContributorAggregate.Specifications;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.Contributors.Get;

/// <summary>
/// Queries don't necessarily to use repository methods, but they can if it's convenient.
/// </summary>
public class GetContributorHandler : IQueryHandler<GetContributorQuery, Result<ContributorDTO>>
{
    private readonly IRepository<Contributor> _repository;
    public GetContributorHandler(IRepository<Contributor> repository) => _repository = repository;
    
    public async Task<Result<ContributorDTO>> Handle(GetContributorQuery request, CancellationToken cancellationToken)
    {
        var specification = new ContributorByIdSpec(request.ContributorId);
        var entity = await _repository.GetByIdAsync(specification, cancellationToken);
        if (entity is null) return Result.NotFound();
        return new ContributorDTO(entity.Id, entity.Name);
    }
}