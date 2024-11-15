using MarketoTest2.Models;

public class ProductCategory
{
    public int ProductId { get; set; }
    public required Product Product { get; set; }

    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}
