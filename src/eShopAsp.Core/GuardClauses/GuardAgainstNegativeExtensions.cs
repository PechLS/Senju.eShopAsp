namespace eShopAsp.Core.GuardClauses;


public static partial class GuardClauseExtensions
{
    private static T Negative<T>(
        this IGuardClause guardClause,
        T input,
        string paramName,
        string? message = null) where T : struct, IComparable
    {
        if (input.CompareTo(default(T)) < 0)
            throw new ArgumentException(message ?? $"Required input {paramName} cannot be negative.");
        return input;
    }

    public static int Negative(this IGuardClause guardClause, int input, string paramName, string? message = null)
        => Negative<int>(guardClause, input, paramName, message);

    public static long Negative(this IGuardClause guardClause, long input, string paramName, string? message = null)
        => Negative<long>(guardClause, input, paramName, message);

    public static decimal Negative(this IGuardClause guardClause, decimal input, string paramName, string? message = null)
        => Negative<decimal>(guardClause, input, paramName, message);

    public static float Negative(this IGuardClause guardClause, float input, string paramName, string? message)
        => Negative<float>(guardClause, input, paramName, message);

    public static double Negative(this IGuardClause guardClause, double input, string paramName, string? message)
        => Negative<double>(guardClause, input, paramName, message);

    public static TimeSpan Negative(this IGuardClause guardClause, TimeSpan input, string paramName, string? message)
        => Negative<TimeSpan>(guardClause, input, paramName, message);


    private static T NegativeOrZero<T>(
        this IGuardClause guardClause,
        T input,
        string paramName,
        string? message = null) where T : struct, IComparable
    {
        if (input.CompareTo(default(T)) <= 0)
            throw new ArgumentException(message ?? $"Required input {paramName} cannot be zero or negative.");
        return input;
    }

    public static int NegativeOrZero(IGuardClause guardClause, int input, string paramName, string? message = null)
        => NegativeOrZero<int>(guardClause, input, paramName, message);
    
    public static long NegativeOrZero(IGuardClause guardClause, long input, string paramName, string? message = null)
        => NegativeOrZero<long>(guardClause, input, paramName, message);
    
    public static decimal NegativeOrZero(IGuardClause guardClause, decimal input, string paramName, string? message = null)
        => NegativeOrZero<decimal>(guardClause, input, paramName, message);
    
    public static float NegativeOrZero(IGuardClause guardClause, float input, string paramName, string? message = null)
        => NegativeOrZero<float>(guardClause, input, paramName, message);
    
    public static double NegativeOrZero(IGuardClause guardClause, double input, string paramName, string? message = null)
        => NegativeOrZero<double>(guardClause, input, paramName, message);
    
    public static TimeSpan NegativeOrZero(IGuardClause guardClause, TimeSpan input, string paramName, string? message = null)
        => NegativeOrZero<TimeSpan>(guardClause, input, paramName, message);
}