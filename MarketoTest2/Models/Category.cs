public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public virtual required ICollection<ProductCategory> ProductCategories { get; set; }
}
