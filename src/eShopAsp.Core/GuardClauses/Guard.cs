namespace eShopAsp.Core.GuardClauses;


/// <summary>
/// Simple interface to provide a generic mechanism to
/// build guard clause extension method from.
/// </summary>
public interface IGuardClause
{
    
}

/// <summary>
/// An entry point to set of Guard Clauses defined as extension methods
/// on IGuardClauses. 
/// </summary>
public class Guard : IGuardClause
{

    /// <summary>
    /// An entry point to a set of GuardClauses.
    /// </summary>
    public static IGuardClause Against { get; } = new Guard();
    private Guard(){}
}