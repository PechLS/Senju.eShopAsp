using System.Linq.Expressions;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Builders;

namespace eShopAsp.Core;

public class SingleResultSpecification<T, TResult> : Specification<T, TResult>, ISingleResultSpecification<T, TResult>
{

}


public class SingleResultSpecification<T> : Specification<T>, ISingleResultSpecification<T>
{
    
}