using System.Linq.Expressions;
using System.Reflection;
using eShopAsp.Core.Exceptions;
using eShopAsp.Core.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Core.Extensions;

public static class SearchExtensions
{
    private static readonly MethodInfo _likeMethodInfo = typeof(DbFunctionsExtensions)
        .GetMethod(nameof(DbFunctionsExtensions.Like),
            new Type[] { typeof(DbFunctions), typeof(string), typeof(string) })
        ?? throw new TargetException("The EF.Functions.Like not found");

    private static readonly MemberExpression _functions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions)))
        ?? throw new TargetException("The EF.Functions not found.");

    public static IQueryable<T> Search<T>(this IQueryable<T> source, IEnumerable<SearchExpressionInfo<T>> criterias)
    {
        Expression? expr = null;
        var parameter = Expression.Parameter(typeof(T), "x");
        foreach (var criteria in criterias)
        {
            if (string.IsNullOrEmpty(criteria.SearchTerm)) continue;
            var propertySelector = ParameterReplacerVisitor
                    .Replace(criteria.Selector, criteria.Selector.Parameters[0], parameter) 
                    as LambdaExpression;
            var searchTermAsExpression = ((Expression<Func<string>>)(() => criteria.SearchTerm)).Body;
            var likeExpression = Expression.Call(
                null,
                _likeMethodInfo,
                _functions,
                propertySelector.Body,
                searchTermAsExpression);
            expr = expr == null ? (Expression)likeExpression : Expression.OrElse(expr, likeExpression);
        }

        return expr == null
            ? source
            : source.Where(Expression.Lambda<Func<T, bool>>(expr, parameter));
    }
    
    public static bool Like(this string input, string pattern)
    {
        try
        {
            return SqlLike(input, pattern);
        }
        catch (Exception)
        {
            throw new InvalidSearchPatternException(pattern);
        }
    }
    
    private static bool SqlLike(string str, string pattern)
    {
        var isMatch = true;
        var isWildCartOn = false;
        var isCharWildCartOn = false;
        var isCharSetOn = false;
        var isNotCharSetOn = false;
        var lastWildCart = -1;
        var patternIndex = 0;
        var set = new List<char>();
        var p = '\0';
        bool endOfPattern;

        for (var i = 0; i < str.Length; i++)
        {
            var c = str[i];
            endOfPattern = patternIndex >= pattern.Length;
            if (!endOfPattern)
            {
                p = pattern[patternIndex];
                if (!isWildCartOn && p == '%')
                {
                    lastWildCart = patternIndex;
                    isWildCartOn = true;
                    while (patternIndex < pattern.Length && pattern[patternIndex] == '%')
                    {
                        patternIndex++;
                    }

                    p = patternIndex >= pattern.Length ? '\0' : pattern[patternIndex];
                }
                else if (p == '_')
                {
                    isCharWildCartOn = true;
                    patternIndex++;
                }
                else if (p == '[')
                {
                    if (pattern[++patternIndex] == '^')
                    {
                        isNotCharSetOn = true;
                        patternIndex++;
                    }
                    else isCharSetOn = true;
                    
                    set.Clear();
                    if (pattern[patternIndex + 1] == '-' && pattern[patternIndex + 3] == ']')
                    {
                        var start = char.ToUpper(pattern[patternIndex]);
                        patternIndex += 2;
                        var end = char.ToUpper(pattern[patternIndex]);
                        if (start <= end)
                        {
                            for (var ci = start; ci <= end; ci++)
                            {
                                set.Add(ci);
                            }
                        }
                        patternIndex++;
                    }

                    while (patternIndex <= pattern.Length && pattern[patternIndex] != ']')
                    {
                        set.Add(pattern[patternIndex]);
                        patternIndex++;
                    }

                    patternIndex++;
                }
            }

            if (isWildCartOn)
            {
                if (char.ToUpper(c) == char.ToUpper(p))
                {
                    isWildCartOn = false;
                    patternIndex++;
                }
            }
            else if (isCharWildCartOn)
            {
                isCharWildCartOn = false;
            }
            else if (isCharSetOn || isNotCharSetOn)
            {
                var charMatch = (set.Contains(char.ToUpper(c)));
                if ((isNotCharSetOn && charMatch) || (isCharSetOn && !charMatch))
                {
                    if (lastWildCart >= 0) patternIndex = lastWildCart;
                    else
                    {
                        isMatch = false;
                        break;
                    }
                }

                isNotCharSetOn = isCharSetOn = false;
            }
            else
            {
                if (char.ToUpper(c) == char.ToUpper(p))
                {
                    patternIndex++;
                }
                else
                {
                    if (lastWildCart >= 0) patternIndex = lastWildCart;
                    else
                    {
                        isMatch = false;
                        break;
                    }
                }
            }
        }

        endOfPattern = patternIndex >= pattern.Length;
        if (isMatch && !endOfPattern)
        {
            var isOnlyWildCarts = true;
            for (var i = patternIndex; i < pattern.Length; i++)
            {
                if (pattern[i] != '%')
                {
                    isOnlyWildCarts = false;
                    break;
                }
            }

            if (isOnlyWildCarts) endOfPattern = true;
        }

        return isMatch && endOfPattern;
    }
}