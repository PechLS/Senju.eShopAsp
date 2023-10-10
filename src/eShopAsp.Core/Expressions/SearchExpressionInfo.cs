using System.Linq.Expressions;

namespace eShopAsp.Core.Expressions;

/// <summary>
/// Encapsulates data needed to perform 'SQL LIKE' operation.
/// </summary>
public class SearchExpressionInfo<T>
{
    private readonly Lazy<Func<T, string>> _selectorFunc;
    
    /// <summary>
    /// The property to apply SQL LIKE against
    /// </summary>
    public Expression<Func<T, string>> Selector { get; }
    
    /// <summary>
    /// The value use for the SQL LIKE
    /// </summary>
    public string SearchTerm { get; }
    
    /// <summary>
    /// the index used to group sets of Selectors and SearchTerms together.
    /// </summary>
    public int SearchGroup { get; }

    /// <summary>
    /// Compiled <see cref="Selector"/>
    /// </summary>
    public Func<T, string> SelectorFunc => _selectorFunc.Value;

    public SearchExpressionInfo(Expression<Func<T, string>> selector, string searchTerm, int searchGroup = 1)
    {
        _ = selector ?? throw new ArgumentNullException(nameof(selector));
        if (string.IsNullOrEmpty(searchTerm)) throw new ArgumentException("The searchTerm cannot be null or empty.");
        Selector = selector;
        SearchTerm = searchTerm;
        SearchGroup = searchGroup;
        _selectorFunc = new Lazy<Func<T, string>>(Selector.Compile);
    }
}