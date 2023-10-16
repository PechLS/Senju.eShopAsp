using eShopAsp.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Infrastructure.Data.Queries;

public class BasketQueryService : IBasketQueryService
{
    private readonly CatalogContext _dbContext;

    public BasketQueryService(CatalogContext dbContext) => _dbContext = dbContext;
    
    public async Task<int> CountTotalBasketItems(string username)
    {
        var totalItems = await _dbContext.Baskets
            .Where(basket => basket.BuyerId == username)
            .SelectMany(item => item.Items)
            .SumAsync(sum => sum.Quantity);
        return totalItems;
    }
}