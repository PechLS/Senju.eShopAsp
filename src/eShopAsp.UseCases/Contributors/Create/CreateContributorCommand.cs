using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.Contributors.Create;

public record CreateContributorCommand(string Name) : ICommand<Result<int>>;