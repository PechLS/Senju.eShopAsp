using FastEndpoints;
using FluentValidation;

namespace eShopAsp.PublicApi.ContributorEndpoints.GetById;

public class GetContributorValidator : Validator<GetContributorByIdRequest>
{
    public GetContributorValidator()
    {
        RuleFor(x => x.ContributorId).GreaterThan(0);
    }
}