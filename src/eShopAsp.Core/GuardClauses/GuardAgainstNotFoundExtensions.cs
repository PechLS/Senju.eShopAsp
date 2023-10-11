using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using eShopAsp.Core.Exceptions;

namespace eShopAsp.Core.GuardClauses;

public static partial class GuardClauseExtensions
{
    public static T NotFound<T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] string key,
        [NotNull] [ValidatedNotNull] T? input,
        [CallerArgumentExpression("input")] string? paramName = null)
    {
        guardClause.NullOrEmpty(key, nameof(key));
        if (input is null) throw new NotFoundException(key, paramName!);
        return input;
    }

    public static T NotFound<TKey, T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] TKey key,
        [NotNull] [ValidatedNotNull] T? input,
        [CallerArgumentExpression("input")] string? paramName = null) where TKey : struct
    {
        guardClause.Null(key, nameof(key));
        if (input is null)
            // TODO: Can we safely consider that ToString() won't return null for struct?
            throw new NotFoundException(key.ToString()!, paramName!);
        return input;
    }
}