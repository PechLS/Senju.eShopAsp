using eShopAsp.UseCases.Contributors;
using eShopAsp.UseCases.Contributors.List;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Infrastructure.Data.Queries;

public class ListContributorsQueryService : IListContributorsQueryService
{
    private readonly AppDbContext _db;
    public ListContributorsQueryService(AppDbContext dbContext) => _db = dbContext;

    public async Task<IEnumerable<ContributorDTO>> ListAsync()
    {
        var result = await _db.Contributors.FromSqlRaw("SELECT Id, Name FROM Contributors")
            .Select(c => new ContributorDTO(c.Id, c.Name))
            .ToListAsync();
        return result;
    }
}