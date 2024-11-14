using Domain;
using Domain.Services;

namespace Persistence;

public class ProductRepository : IProductRepository
{
    private readonly SqlServerDb _context;

    public ProductRepository(SqlServerDb context)
    {
        _context = context;
    }

    public ICollection<Product?> GetProducts()
    {
        return _context.Products.ToList()!;
    }

    public Product? GetProduct(int id)
    {
        return _context.Products.FirstOrDefault(p => p != null && p.Id == id);
    }

    public void CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}