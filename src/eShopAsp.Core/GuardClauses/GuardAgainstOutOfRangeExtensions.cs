using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace eShopAsp.Core.GuardClauses;

public static partial class GuardClauseExtensions
{
    public static int EnumOutOfRange<T>(
        this IGuardClause guardClause,
        int input,
        string paramName,
        string? message = null) where T : struct, Enum
    {
        if (!Enum.IsDefined(typeof(T), input))
        {
            if (string.IsNullOrEmpty(message)) throw new InvalidEnumArgumentException(paramName, input, typeof(T));
            throw new InvalidEnumArgumentException(message);
        }
        return input;
    }

    public static T EnumOurOfRange<T>(
        this IGuardClause guardClause,
        T input,
        string paramName,
        string? message = null) where T : struct, Enum
    {
        if (!Enum.IsDefined(typeof(T), input))
        {
            if (string.IsNullOrEmpty(message))
                throw new InvalidEnumArgumentException(paramName, Convert.ToInt32(input), typeof(T));
            throw new InvalidEnumArgumentException(message);
        }

        return input;
    }

    public static IEnumerable<T> OutOfRange<T>(
        this IGuardClause guardClause,
        IEnumerable<T> input,
        string paramName,
        T rangeFrom, T rangeTo,
        string? message = null) where T : IComparable, IComparable<T>
    {
        if (rangeFrom.CompareTo(rangeTo) > 0)
            throw new ArgumentException(
                message ?? $"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}", paramName);
        if (input.Any(x => x.CompareTo(rangeFrom) <= 0 || x.CompareTo(rangeTo) >= 0))
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentOutOfRangeException(paramName,
                    message ?? $"Input {paramName} had out of range item(s)");
            throw new ArgumentOutOfRangeException(paramName, message);
        }

        return input;
    }

    public static DateTime NullOrOutOfSqlDateRange(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] DateTime? input,
        string paramName,
        string? message = null)
    {
        guardClause.Null(input, nameof(input));
        return OutOfSqlDateRange(guardClause, input.Value, paramName, message);
    } 
    
    public static DateTime OutOfSqlDateRange(
        this IGuardClause guardClause,
        DateTime input,
        string paramName,
        string? message = null)
    {
        // System.Data is unavailable in .NET standard so we can't use SQLDateTime.
        const long sqlMinDateTicks = 552877920000000000;
        const long sqlMaxDateTicks = 3155378975999970000;
        return NullOrOutOfRangeInternal<DateTime>
        (guardClause, input, paramName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks), message);
    }

    public static T OutOfRange<T>(
        this IGuardClause guardClause,
        T input,
        string paramName,
        [NotNull] [ValidatedNotNull] T rangeFrom,
        [NotNull] [ValidatedNotNull] T rangeTo,
        string? message = null) where T : IComparable, IComparable<T>
    {
        return NullOrOutOfRangeInternal<T>(guardClause, input, paramName, rangeFrom, rangeTo, message);
    }

    public static T NullOrOutOfRange<T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] T? input,
        string paramName,
        [NotNull] [ValidatedNotNull] T rangeFrom,
        [NotNull] [ValidatedNotNull] T rangeTo,
        string? message = null) where T : IComparable<T>
    {
        guardClause.Null(input, nameof(input));
        return NullOrOutOfRangeInternal<T>(guardClause, input, paramName, rangeFrom, rangeTo, message);
    }
    
    public static T NullOrOutOfRange<T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] T? input,
        string paramName,
        [NotNull] [ValidatedNotNull] T rangeFrom,
        [NotNull] [ValidatedNotNull] T rangeTo,
        string? message = null) where T : struct, IComparable<T>
    {
        guardClause.Null(input, nameof(input));
        return NullOrOutOfRangeInternal<T>(guardClause, input.Value, paramName, rangeFrom, rangeTo, message);
    }

    private static T NullOrOutOfRangeInternal<T>(
        this IGuardClause guardClause,
        [NotNull] [ValidatedNotNull] T? input,
        string? paramName,
        [NotNull] [ValidatedNotNull] T? rangeFrom,
        [NotNull] [ValidatedNotNull] T? rangeTo,
        string? message = null) where T : IComparable<T>?
    {
        Guard.Against.Null(input, nameof(input));
        Guard.Against.Null(paramName, nameof(paramName));
        Guard.Against.Null(rangeFrom, nameof(rangeFrom));
        Guard.Against.Null(rangeTo, nameof(rangeTo));

        if (rangeFrom.CompareTo(rangeTo) > 0)
            throw new ArgumentException(
                message ?? $"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}", paramName);
        if (input.CompareTo(rangeFrom) < 0 || input.CompareTo(rangeTo) > 0)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentOutOfRangeException(paramName, $"Input {paramName} was out of range");
            throw new ArgumentOutOfRangeException(paramName, message);
        }
        return input;
    }
}