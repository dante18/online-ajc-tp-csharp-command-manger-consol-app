using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class FoodRepository
{
    private readonly CommandStoreContext _context;

    public FoodRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Food> GetAllFoods()
    {
        return this._context.Food.ToList();
    }

    public Food GetFood(int id)
    {
        return this._context.Food.Where(f => f.Id == id).FirstOrDefault();
    }
}
