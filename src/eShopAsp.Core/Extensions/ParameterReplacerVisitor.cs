using System.Linq.Expressions;

namespace eShopAsp.Core.Extensions;

internal class ParameterReplacerVisitor : ExpressionVisitor
{
    private readonly Expression _newExpression;
    private readonly ParameterExpression _oldExpression;

    public ParameterReplacerVisitor(ParameterExpression oldExpression, Expression newExpression)
    {
        _oldExpression = oldExpression;
        _newExpression = newExpression;
    }

    internal static Expression Replace(Expression expression, ParameterExpression oldExpression, Expression newExpression)
        => new ParameterReplacerVisitor(oldExpression, newExpression).Visit(expression);

    protected override Expression VisitParameter(ParameterExpression p)
        => p == _oldExpression ? _newExpression : p;
}