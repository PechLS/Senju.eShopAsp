using System.Text.RegularExpressions;

namespace  eShopAsp.Core.GuardClauses;

public static partial class GuardClauseExtensions
{
    public static string InvalidFormat(
        this IGuardClause guardClause,
        string input,
        string paramName,
        string regexPattern,
        string? message = null)
    {
        var m = Regex.Match(input, regexPattern);
        if (!m.Success || input != m.Value)
            throw new ArgumentException(message ?? $"Input {paramName} was not in required format.", paramName);
        return input;
    }

    public static T InvalidInput<T>(
        this IGuardClause guardClause,
        T input,
        string paramName,
        Func<T, bool> predicate,
        string? message = null)
    {
        if (!predicate(input))
            throw new ArgumentException(message ?? $"Input {paramName} did not satisfy the options.", paramName);
        return input;
    }

    public static async Task<T> InvalidInputAsync<T>(
        this IGuardClause guardClause,
        T input,
        string paramName,
        Func<T, Task<bool>> predicate,
        string? message = null)
    {
        if (!await predicate(input))
            throw new ArgumentException(message ?? $"Input {paramName} did not satisfy the options.", paramName);
        return input;
    }
}
