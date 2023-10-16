namespace eShopAsp.Core.Entities.BuyerAggregate;

public class PaymentMethod : EntityBase
{
    public string? Alias { get; private set; }
    // actual card data must be stored in a PCI compliant system, like Stripe
    public string? CardId { get; private set; }
    public string? Last4 { get; private set; }
    
}