using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class DoughsRepository
{
    private readonly CommandStoreContext _context;

    public DoughsRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Doughs> GetAllDoughs()
    {
        return this._context.Doughs.ToList();
    }

    public Doughs GetDoughs(int id)
    {
        return this._context.Doughs.FirstOrDefault(d => d.Id == id);
    }

    public void CreateDoughs(Doughs doughs)
    {
        this._context.Doughs.Add(doughs);
        this._context.SaveChanges();
    }

    public void UpdateDoughs(Doughs doughs)
    {
        this._context.Doughs.Update(doughs);
        this._context.SaveChanges();
    }

    public void DeleteDoughs(Doughs doughs)
    {
        this._context.Doughs.Remove(doughs); 
        this._context.SaveChanges();
    }
}
