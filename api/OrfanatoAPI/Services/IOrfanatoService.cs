using OrfanatoAPI.DTOs;
using OrfanatoAPI.Models;
using OrfanatoAPI.Requests;
using OrfanatoAPI.Response;

namespace OrfanatoAPI.Services;

public interface IOrfanatoService
{
    OrfanatoByIdDTO GetById(int id);
    List<OrfanatoMapDTO> GetAllActives();
    List<OrphanageCardsDTO> GetAllOrphanages();
    Task<ValidationResponse<OrfanatoDTO>> InsertAsync(InsertOrfanatoRequest request);
    Task<ToggleResponse<Orphanage>> UpdateAtivo(UpdateAtivoRequest request);
}
