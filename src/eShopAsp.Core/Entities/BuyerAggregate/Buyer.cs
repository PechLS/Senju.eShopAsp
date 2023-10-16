using eShopAsp.Core.GuardClauses;
using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.Entities.BuyerAggregate;

public class Buyer : EntityBase, IAggregateRoot
{
    public string IdentityGuid { get; private set; }
    private List<PaymentMethod> _paymentMethods = new();
    public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();
    
    private Buyer(){}

    public Buyer(string identity) : this()
    {
        Guard.Against.NullOrEmpty(identity, nameof(identity));
        IdentityGuid = identity;
    }

}