using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class PastaService
{
    private readonly PastaRepository _repository;

    public PastaService(CommandStoreContext context)
    {
        this._repository = new PastaRepository(context);
    }

    public List<PastaDto> GetAllPastas()
    {
        List<PastaDto> pastas = this._repository.GetAllPastas().Select(p => new PastaDto()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Type = p.Type,
            KCal = p.KCal,
            Vegetarian = p.Vegetarian,
        }).ToList();

        return pastas;
    }

    public PastaDto GetPasta(int id)
    {
        Pasta pasta = this._repository.GetPasta(id);

        if (pasta is null)
        {
            return null;
        }

        PastaDto pastaDto = new PastaDto()
        {
            Id = pasta.Id,
            Name = pasta.Name,
            Price = pasta.Price,
            Type = pasta.Type,
            KCal = pasta.KCal,
            Vegetarian = pasta.Vegetarian,
        };

        return pastaDto;
    }

    public void CreatePasta(PastaDto pasta)
    {
        Pasta newPasta = new Pasta()
        {
            Id = pasta.Id,
            Name = pasta.Name,
            Price = pasta.Price,
            Type = pasta.Type,
            KCal = pasta.KCal,
            Vegetarian = pasta.Vegetarian,
        };

        this._repository.CreatePasta(newPasta);
    }

    public void UpdatePasta(int id, PastaDto data)
    {
        Pasta existingPasta = this._repository.GetPasta(id);
        existingPasta.Name = data.Name;
        existingPasta.Price = data.Price;
        existingPasta.Vegetarian = data.Vegetarian;

        this._repository.UpdatePasta(existingPasta);
    }

    public void DeletePasta(int id)
    {
        Pasta existingPasta = this._repository.GetPasta(id);
        this._repository.DeletePasta(existingPasta);
    }
}