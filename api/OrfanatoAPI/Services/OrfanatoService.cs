using FluentValidation;
using OrfanatoAPI.DTOs;
using OrfanatoAPI.Models;
using OrfanatoAPI.Repositories;
using OrfanatoAPI.Requests;
using OrfanatoAPI.Response;

namespace OrfanatoAPI.Services;

public class OrfanatoService : IOrfanatoService
{
    public IOrfanatoRepository OrfanatoRepository { get; }
    private IValidator<Orphanage> OrfanatoValidator { get; }

    public OrfanatoService(
        IOrfanatoRepository orfanatoRepository,
        IValidator<Orphanage> orfanatoValidator
        )
    {
        OrfanatoRepository = orfanatoRepository;
        OrfanatoValidator = orfanatoValidator;
    }

    public OrfanatoByIdDTO GetById(int id)
    {
        var entity = OrfanatoRepository.GetById(id);
        var imagensDto = entity.Imagens.Select(x => new ImageDTO(x)).ToList();

        return new OrfanatoByIdDTO
        {
            Name = entity.Name,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            Instructions = entity.Instructions,
            About = entity.About,
            Whatsapp = entity.Whatsapp,
            AbreAs = entity.AbreAs,
            FechaAs = entity.FechaAs,
            OpenOnWeekends = entity.AbertoFimDeSemana,
            Images = imagensDto
        };
    }

    public List<OrfanatoMapDTO> GetAllActives() =>
        OrfanatoRepository
        .GetAll()
        .Where(x => x.Ativo is true)
        .OrderBy(x => x.Id)
        .Select(x => new OrfanatoMapDTO(x))
        .ToList();

    public List<OrphanageCardsDTO> GetAllOrphanages() =>
       OrfanatoRepository
       .GetAll()
       .Select(x => new OrphanageCardsDTO(x))
       .ToList();

    public async Task<ValidationResponse<OrfanatoDTO>> InsertAsync(InsertOrfanatoRequest request)
    {
        var orfanato = request.GetEntity();
        //var imagens = request.Images.Select(x => new Image(x)).ToList();
        //orfanato.Imagens = imagens;
        var orfanatoValidationResult = OrfanatoValidator.Validate(orfanato);
        if (orfanatoValidationResult.IsValid)
        {
            var orfanatoEntity = await OrfanatoRepository.CreateAsync(orfanato);
            var dto = new OrfanatoDTO(orfanatoEntity);
            return ValidationResponse<OrfanatoDTO>.Valid(orfanatoValidationResult, dto);
        }
        else
        {
            return ValidationResponse<OrfanatoDTO>.Invalid(orfanatoValidationResult);
        }
    }

    public async Task<ToggleResponse<Orphanage>> UpdateAtivo(UpdateAtivoRequest request)
    {
        try
        {
            var orfanato = OrfanatoRepository.GetById(request.Id);
            if (orfanato is null)
            {
                return ToggleResponse<Orphanage>.Invalid(new List<string> { "id não existente" });
            }
            else if (orfanato.Ativo is false)
            {
                orfanato.Ativo = true;
                var orfanatoAtivado = await OrfanatoRepository.UpdateAtivo(orfanato);
                return ToggleResponse<Orphanage>.Valid(orfanatoAtivado.Id);
            }
            orfanato.Ativo = false;
            var orfanatoDesativado = await OrfanatoRepository.UpdateAtivo(orfanato);
            return ToggleResponse<Orphanage>.Valid(orfanatoDesativado.Id);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
