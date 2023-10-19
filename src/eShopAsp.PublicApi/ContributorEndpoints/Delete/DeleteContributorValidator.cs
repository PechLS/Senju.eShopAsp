using FastEndpoints;
using FluentValidation;

namespace eShopAsp.PublicApi.ContributorEndpoints.Delete;

public class DeleteContributorValidator : Validator<DeleteContributorRequest>
{
    public DeleteContributorValidator()
    {
        RuleFor(x => x.ContributorId)
            .GreaterThan(0);
    }
}