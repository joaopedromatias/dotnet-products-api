public class ActiveProduct(int activeProductId, int userId, int productId, int quantity, double pricePayed) {

    public int ActiveProductId {get;} = activeProductId;
    public int UserId {get;} = userId;
    public int ProductId {get;} = productId;
    public int Quantity {get;set;} = quantity;
    public double PricePayed {get; } = pricePayed;
    public DateOnly BuyDate {get;} = DateOnly.FromDateTime(DateTime.Now);
    
}
