namespace Domain.Services;

public interface IProductRepository
{
    public ICollection<Product?> GetProducts();
    public Product? GetProduct(int id);
    public void CreateProduct(Product product);
    public void UpdateProduct(Product product);
    public void DeleteProduct(Product product);
}