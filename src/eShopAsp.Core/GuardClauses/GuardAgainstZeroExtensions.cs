using System.Runtime.CompilerServices;

namespace eShopAsp.Core.GuardClauses;

public static partial class GuardClauseExtensions
{
    private static T Zero<T>(
        this IGuardClause guardClause,
        T input,
        string paramName,
        string? message = null) where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(input, default(T)))
            throw new ArgumentException(message ?? $"Required input {paramName} cannot be zero.", paramName);
        return input;
    }

    public static int Zero(this IGuardClause guardClause, int input, [CallerArgumentExpression("input")] string? paramName = null, string? message = null)
        => Zero<int>(guardClause, input, paramName!, message);

    public static long Zero(this IGuardClause guardClause, long input, [CallerArgumentExpression("input")] string? paramName = null, string? message = null)
        => Zero<long>(guardClause, input, paramName!, message);

    public static decimal Zero(this IGuardClause guardClause, decimal input, [CallerArgumentExpression("input")] string? paramName = null, string? message = null)
        => Zero<decimal>(guardClause, input, paramName!, message);

    public static float Zero(this IGuardClause guardClause, float input, [CallerArgumentExpression("input")] string? paramName = null, string? message = null)
        => Zero<float>(guardClause, input, paramName!, message);
    
    public static double Zero(this IGuardClause guardClause, double input, [CallerArgumentExpression("input")] string? paramName = null, string? message = null)
        => Zero<double>(guardClause, input, paramName!, message);

    public static TimeSpan Zero(this IGuardClause guardClause, TimeSpan input, [CallerArgumentExpression("input")] string? paramName = null)
        => Zero<TimeSpan>(guardClause, input, paramName!);


}
