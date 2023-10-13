namespace eShopAsp.Core.Interfaces.Services;

public interface IContributorService
{
    // this service and method exist to provide a place in which to fire 
    // domain events when deleting this aggregate root entity 
    public Task<Core.Result.Result> DeleteContributor(int contributorId);
}