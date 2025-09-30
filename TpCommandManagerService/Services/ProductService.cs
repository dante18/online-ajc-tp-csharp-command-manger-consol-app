using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class ProductService
{
    private readonly ProductRepository _repository;

    public ProductService(CommandStoreContext context)
    {
        this._repository = new ProductRepository(context);
    }

    public List<ProductDto> GetAllProducts()
    {
        List<ProductDto> products = this._repository.GetAllProducts().Select(p => new ProductDto()
        {
            Id = p.Id ?? 0,
            Name = p.Name,
            Price = p.Price,
        }).ToList();

        return products;
    }

    public ProductDto? GetProduct(int id)
    {
        Product product = this._repository.GetProduct(id);

        if (product is null)
        {
            return null;
        }

        ProductDto productDto = new ProductDto()
        {
            Id = product.Id ?? 0,
            Name = product.Name,
            Price = product.Price,
        };

        return productDto;
    }
}
