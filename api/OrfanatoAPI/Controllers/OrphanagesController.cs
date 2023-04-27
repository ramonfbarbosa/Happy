using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrfanatoAPI.DTOs;
using OrfanatoAPI.Requests;
using OrfanatoAPI.Services;

namespace OrfanatoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrphanageController : Controller
{
    private ILogger<OrphanageController> Logger { get; }
    public IOrfanatoService OrfanatoService { get; }

    public OrphanageController(IOrfanatoService orfanatoService, ILogger<OrphanageController> logger)
    {
        OrfanatoService = orfanatoService;
        Logger = logger;
    }

    [HttpGet("orphanages/{orfanatoId}")]
    public ActionResult<OrfanatoByIdDTO> GetById(int orfanatoId) =>
        Ok(OrfanatoService.GetById(orfanatoId));

    [HttpGet("orphanages-map")]
    public ActionResult<List<OrfanatoMapDTO>> GetAll() =>
         Ok(OrfanatoService.GetAllActives());

    [HttpGet("orphanages")]
    public ActionResult<OrfanatoByIdDTO> GetAllOrphanages() =>
        Ok(OrfanatoService.GetAllOrphanages());

    [HttpPost("insert")]
    public async Task<IActionResult> InsertOrfanato([FromBody] InsertOrfanatoRequest request)
    {
        try
        {
            var result = await OrfanatoService.InsertAsync(request);
            if (result.Sucesso)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        catch (Exception e)
        {
            string errorMessage = $"InsertOrfanato error - {e.Message}";
            Logger.LogError(e, errorMessage);
            return StatusCode(500, errorMessage);
        }
    }

    [HttpPost("toggle-orphanage")]
    [Authorize]
    public async Task<IActionResult> ToggleOrphanage([FromBody] UpdateAtivoRequest request)
    {
        try
        {
            var result = await OrfanatoService.UpdateAtivo(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        catch (Exception e)
        {
            string errorMessage = $"ActiveOrDesactiveMethod error - {e.Message}";
            Logger.LogError(e, errorMessage);
            return StatusCode(500, errorMessage);
        }
    }
}
