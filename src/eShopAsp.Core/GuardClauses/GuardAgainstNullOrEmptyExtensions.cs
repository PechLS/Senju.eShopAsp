using System.Diagnostics.CodeAnalysis;

namespace eShopAsp.Core.GuardClauses;

public static partial class GuardClauseExtensions
{
    public static T Null<T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] T? input,
        string paramName,
        string? message = null)
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(paramName);
            throw new ArgumentNullException(paramName, message);
        }

        return input;
    }

    public static T Null<T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] T? input,
        string paramName,
        string? message = null) where T : struct
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(paramName);
            throw new ArgumentNullException(paramName, message);
        }

        return input.Value;
    }

    public static string NullOrEmpty(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] string? input,
        string paramName,
        string? message = null)
    {
        Guard.Against.Null(input, paramName, message);
        if (input == String.Empty)
            throw new ArgumentException(message ?? $"Required input {paramName} was empty.", paramName);
        return input;
    }

    public static Guid NullOrEmpty(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] Guid? input,
        string paramName,
        string? message = null)
    {
        Guard.Against.Null(input, paramName, message);
        if (input == Guid.Empty)
            throw new ArgumentException(message ?? $"Required input {paramName} was empty.", paramName);
        return input.Value;
    }

    public static IEnumerable<T> NullOrEmpty<T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] IEnumerable<T>? input,
        string paramName,
        string? message = null)
    {
        Guard.Against.Null(input, paramName, message);
        if (!input.Any())
            throw new ArgumentException(message ?? $"Required input {paramName} was empty.", paramName);
        return input;
    }

    public static string NullOrWhiteSpace(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] string? input,
        string paramName,
        string? message = null)
    {
        Guard.Against.NullOrEmpty(input, paramName, message);
        if (String.IsNullOrWhiteSpace(input))
            throw new ArgumentException(message ?? $"Required input {paramName} was empty.", paramName);
        return input;
    }

    public static T Default<T>(
        this IGuardClause guardClause,
        [AllowNull, NotNull] T input,
        string paramName,
        string? message = null)
    {
        if (EqualityComparer<T>.Default.Equals(input, default(T)!) || input is null)
            throw new ArgumentException(
                message ?? $"Parameter [{paramName}] is default value for type {typeof(T).Name}", paramName);
        return input;
    }

    public static T NullOrInvalidInput<T>(
        this IGuardClause guardClause,
        [NotNull] T? input,
        string paramName,
        Func<T, bool> predicate,
        string? message = null)
    {
        Guard.Against.Null(input, paramName, message);
        return Guard.Against.InvalidInput(input, paramName, predicate, message);
    }

}