public class Product(int id, string name, double rate, int quantity, string category, double price, DateOnly finalDate) {
    public int Id {get; set;} = id;
    public string Name {get;} = name;
    public double Rate {get;} = rate;
    public int Quantity {get;set;} = quantity;
    public string Category {get;} = category;
    public DateOnly CreationDate {get;} = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly FinalDate {get;} = finalDate;
    public double Price {get;} = price;
}
