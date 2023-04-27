using OrfanatoAPI.Models;

namespace OrfanatoAPI.Repositories;

public interface IOrfanatoRepository
{
    Orphanage GetById(int id);
    List<Orphanage> GetAll();
    Task<Orphanage> CreateAsync(Orphanage novoOrfanato);
    Task<Orphanage> UpdateAtivo(Orphanage orfanatoAtualizado);
}
