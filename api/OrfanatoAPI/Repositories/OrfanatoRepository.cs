using Microsoft.EntityFrameworkCore;
using OrfanatoAPI.Context;
using OrfanatoAPI.Models;

namespace OrfanatoAPI.Repositories;

public class OrfanatoRepository : IOrfanatoRepository
{
    private OrfanatoContext OrfanatoContext { get; }

    public OrfanatoRepository(OrfanatoContext orfanatoContext)
    {
        OrfanatoContext = orfanatoContext;
    }

    private IQueryable<Orphanage> OrfanatosWithIncludes =>
           OrfanatoContext.Orfanatos.Include(x => x.Imagens);

    public List<Orphanage> GetAll() =>
            OrfanatosWithIncludes.OrderBy(x => x.Id).ToList();

    public Orphanage GetById(int id) =>
            OrfanatosWithIncludes.AsNoTracking().FirstOrDefault(x => x.Id == id);

    public async Task<Orphanage> CreateAsync(Orphanage novoOrfanato)
    {
        using var transaction = OrfanatoContext.Database.BeginTransaction();
        try
        {
            var proxy = OrfanatoContext.Orfanatos.Add(novoOrfanato);
            await OrfanatoContext.SaveChangesAsync();
            transaction.Commit();
            return proxy.Entity;
        }
        catch (Exception)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception) { }
            throw;
        }
    }

    public async Task<Orphanage> UpdateAtivo(Orphanage orfanatoAtualizado)
    {
        var proxy = OrfanatoContext.Attach(orfanatoAtualizado);
        proxy.State = EntityState.Modified;
        OrfanatoContext.Update(orfanatoAtualizado);
        await OrfanatoContext.SaveChangesAsync();
        return proxy.Entity;
    }
}
