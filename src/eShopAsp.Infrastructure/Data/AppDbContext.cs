using System.Reflection;
using eShopAsp.Core.Entities;
using eShopAsp.Core.Entities.ContributorAggregate;
using eShopAsp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher? dispatcher)
        : base(options) => _dispatcher = dispatcher;

    public DbSet<Contributor> Contributors => Set<Contributor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        int result = await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (_dispatcher == null) return result;
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();
        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        return result;
    }
}