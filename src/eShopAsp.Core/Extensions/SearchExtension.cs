using eShopAsp.Core.Exceptions;
using eShopAsp.Core.Expressions;

namespace eShopAsp.Core.Extensions;

public static class SearchExtensions
{

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