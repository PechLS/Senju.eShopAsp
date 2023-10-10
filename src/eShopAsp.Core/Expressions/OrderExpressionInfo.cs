using System.Linq.Expressions;

namespace eShopAsp.Core.Expressions;

public class OrderExpressionInfo<T>
{
    private readonly Lazy<Func<T, object?>> _keySelectorFunc;
    
    /// <summary>
    /// a function to extract a key from an element
    /// </summary>
    public Expression<Func<T, object?>> KeySelector { get; }
    
    /// <summary>
    /// whether to (subsequently) sort ascending or descending
    /// </summary>
    public OrderTypeEnum OrderType { get; }

    /// <summary>
    /// Complied <see cref="KeySelector"/>
    /// </summary>
    public Func<T, object?> KeySelectorFunc => _keySelectorFunc.Value;

    public OrderExpressionInfo(Expression<Func<T, object?>> keySelector, OrderTypeEnum orderType)
    {
        _ = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        KeySelector = keySelector;
        OrderType = orderType;
        _keySelectorFunc = new Lazy<Func<T, object?>>(KeySelector.Compile);
    }
}