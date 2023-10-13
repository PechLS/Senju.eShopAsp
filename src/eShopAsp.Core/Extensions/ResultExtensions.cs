using eShopAsp.Core.Result;

namespace eShopAsp.Core.Extensions;

public static class ResultExtensions
{
    public static Result<TDestination> Map<TSource, TDestination>(
        this Result<TSource> result,
        Func<TSource, TDestination> func)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok : return func(result);
            case ResultStatus.Unauthorized: return Result<TDestination>.Unauthorized();
            case ResultStatus.Invalid: return Result<TDestination>.Invalid(result.ValidationErrors);
            case ResultStatus.ForBidden: return Result<TDestination>.Forbidden();
            case ResultStatus.Unavailable: return Result<TDestination>.Unavailable(result.Errors.ToArray());
            case ResultStatus.Error: return Result<TDestination>.Error(result.Errors.ToArray());
            case ResultStatus.CriticalError: return Result<TDestination>.Error(result.Errors.ToArray());
            case ResultStatus.NotFound : return result.Errors.Any()
                ? Result<TDestination>.NotFound(result.Errors.ToArray())
                : Result<TDestination>.NotFound();
            case ResultStatus.Conflict: return result.Errors.Any()
                ? Result<TDestination>.Conflict(result.Errors.ToArray())
                : Result<TDestination>.Conflict();
            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }
}