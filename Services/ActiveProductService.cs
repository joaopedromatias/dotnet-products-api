public static class ActiveProductService { 
    
    private static int NextActiveProductId = 1;
    static readonly List<ActiveProduct> ActiveProducts = new List<ActiveProduct>();

    public static List<ActiveProduct>? GetUserActiveProducts (int userId) {
        var userProducts = ActiveProducts.Where(p => p.UserId == userId).ToList();
        return userProducts;
    }

    public static List<ActiveProduct>? GetActiveProductsById (int activeProductId) {
        var userProducts = ActiveProducts.Where(p => p.ActiveProductId == activeProductId).ToList();
        return userProducts;
    }

    public static void AddProductToUser (int userId, int productId, int quantity, double pricePayed) {
        var activeProductToAdd = new ActiveProduct(NextActiveProductId, userId, productId, quantity, pricePayed);
        NextActiveProductId++;
        ActiveProducts.Add(activeProductToAdd);
    }

    public static void RemoveProductFromUser (int activeProductId, int quantity) {
        var activeProductToRemove = ActiveProducts.Where(p => p.ActiveProductId == activeProductId).FirstOrDefault();
        if (activeProductToRemove is not null){ 
            activeProductToRemove.Quantity -= quantity;

            var index = ActiveProducts.FindIndex(p => p.ActiveProductId == activeProductId);

            if (activeProductToRemove.Quantity > 0) { 
                ActiveProducts[index] = activeProductToRemove;
            } else { 
                ActiveProducts.Remove(activeProductToRemove);
            }
        }     
    }

    public static int GetProductIdFromActiveProductId (int activeProductId) {
        var productId = ActiveProducts.Where(p => p.ActiveProductId == activeProductId).Select(p => p.ProductId).FirstOrDefault();
        return productId;
    }
}