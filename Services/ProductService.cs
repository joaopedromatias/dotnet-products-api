public static class ProductService { 
    static readonly List<Product> Products = [
        new (1, "CDB", 4.5, 10,  "Renda Fixa", 1.12, DateOnly.FromDateTime(DateTime.Now.AddDays(10))),
        new(2, "Ação", 4.0, 20,  "Renda Variável", 2.6, DateOnly.FromDateTime(DateTime.Now.AddDays(120))),
        new (3, "Fundo Multimercado", 3.5, 25, "Misto", 1.74, DateOnly.FromDateTime(DateTime.Now.AddDays(100)))
    ];

    static int CurrentId = Products.Count;

    public static List<Product> List(){ 
        return Products;
    }

    public static Product? Get(int id) {
        return Products.FirstOrDefault(p => p!.Id == id, null);
    }

    public static void Create(Product product) {
        int NextId = CurrentId + 1;
        product.Id = NextId;
        Products.Add(product);
        CurrentId++;
    }

    public static bool Update(int id, Product product) {
        int index = Products.FindIndex(p => p.Id == id);

        if (index == -1){ 
            return false;
        }
        product.Id = id;
        Products[index] = product;
        return true;
    }

    public static bool Delete(int id) {
        Product? product = Get(id);
        if (product is null) { 
            return false;
        }
        Products.Remove(product);
        return true;
    }
}