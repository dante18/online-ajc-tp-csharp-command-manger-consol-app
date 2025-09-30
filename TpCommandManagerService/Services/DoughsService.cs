using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;
using TpCommandManagerData.Repositories;
using TpCommandManagerService.Dtos;

namespace TpCommandManagerService.Services;

public sealed class DoughsService
{
    private readonly DoughsRepository _repository;

    public DoughsService(CommandStoreContext context)
    {
        this._repository = new DoughsRepository(context);
    }

    public List<DoughsDto> GetAllDoughs()
    {
        List<DoughsDto> doughs = this._repository.GetAllDoughs().Select(d => new DoughsDto()
        {
            Id = d.Id ?? 0,
            Name = d.Name,
        }).ToList();

        return doughs;
    }

    public DoughsDto GetDoughs(int id)
    {
        Doughs doughs = this._repository.GetDoughs(id);

        if (doughs is null)
        {
            return null;
        }

        DoughsDto doughsDto = new DoughsDto()
        {
            Id = doughs.Id ?? 0,
            Name = doughs.Name,
        };

        return doughsDto;
    }

    public void CreateDoughs(DoughsDto doughs)
    {
        Doughs newDoughs = new Doughs()
        {
            Name = doughs.Name
        };

        this._repository.CreateDoughs(newDoughs);
    }

    public void UpdateDoughs(int id, DoughsDto data)
    {
        Doughs existingDoughs = this._repository.GetDoughs(id);
        existingDoughs.Name = data.Name;

        this._repository.UpdateDoughs(existingDoughs);
    }

    public void DeleteDoughs(int id)
    {
        Doughs existingDoughs = this._repository.GetDoughs(id);
        this._repository.DeleteDoughs(existingDoughs);
    }
}

