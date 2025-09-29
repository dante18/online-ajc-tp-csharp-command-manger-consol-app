using Microsoft.EntityFrameworkCore;
using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class ProductRepository
{
    private readonly CommandStoreContext _context;

    public ProductRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Product> GetAllProducts()
    {
        return this._context.Product.Include(p => p.OrderProduct).ToList();
    }

    public Product GetProduct(int id)
    {
        return this._context.Product.Include(p => p.OrderProduct).Where(p => p.Id == id).FirstOrDefault();
    }
}
