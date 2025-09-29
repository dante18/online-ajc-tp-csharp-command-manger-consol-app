using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class PastaRepository
{
    private readonly CommandStoreContext _context;

    public PastaRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<Pasta> GetAllPastas()
    {
        return this._context.Pastas.ToList();
    }

    public Pasta GetPasta(int id)
    {
        return this._context.Pastas.Where(p => p.Id == id).FirstOrDefault();
    }

    public void CreatePasta(Pasta pasta)
    {
        this._context.Pastas.Add(pasta);
        this._context.SaveChanges();
    }

    public void UpdatePasta(Pasta pasta)
    {
        this._context.Pastas.Update(pasta);
        this._context.SaveChanges();
    }

    public void DeletePasta(Pasta pasta)
    {
        this._context.Pastas.Remove(pasta);
        this._context.SaveChanges();
    }
}
