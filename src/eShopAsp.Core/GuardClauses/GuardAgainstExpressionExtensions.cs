namespace eShopAsp.Core.GuardClauses;

public static partial class GuardClauseExtensions
{
    public static T AgainstExpression<T>(
        this IGuardClause guardClause,
        Func<T, bool> func,
        T input,
        string message) where T : struct
    {
        if (!func(input)) throw new ArgumentException(message);
        return input;
    }

    public static async Task<T> AgainstExpressionAsync<T>(
        this IGuardClause guardClause,
        Func<T, Task<bool>> func,
        T input,
        string message) where T : struct
    {
        if (!await func(input)) throw new ArgumentException(message);
        return input;
    }

    public static T AgainstExpression<T>(
        this IGuardClause guardClause,
        Func<T, bool> func,
        T input,
        string message,
        string paramName) where T : struct
    {
        if (!func(input)) throw new ArgumentException(message, paramName);
        return input;
    } 
}