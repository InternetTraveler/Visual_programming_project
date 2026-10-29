using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Presentetion.Requests.Graf;
using VisualProgramming.Presentetion.Responses.ElementGraf;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.Graf;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/graf/[controller]")]
public class GrafsController(IGrafAplicationService grafsApplicationService,
                             IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GrafResponse>))]
    public async Task<IActionResult> GetAllGrafs(CancellationToken cancellationToken)
    {
        var grafs = await grafsApplicationService.GetModelsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<GrafResponse>>(grafs));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GrafResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetGrafById(Guid id, CancellationToken cancellationToken)
    {
        var graf = await grafsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (graf is null)
            return NotFound($"Graf with id:{id} not found");
        return Ok(mapper.Map<GrafResponse>(graf));
    }

    // просмотр всех элементов графа
    [HttpGet("{id:guid}/elements")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ElementGrafResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetGrafElements(Guid id, CancellationToken cancellationToken)
    {
        var graf = await grafsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (graf is null)
            return NotFound($"Graf with id:{id} not found");

        var elements = await grafsApplicationService.GetElementsAsync(id, cancellationToken);
        return Ok(mapper.Map<IEnumerable<ElementGrafResponse>>(elements));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GrafResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateGraf(CreateGrafRequest request, CancellationToken cancellationToken)
    {
        var graf = mapper.Map<CreateGrafModel>(request);
        var createdGraf = await grafsApplicationService.CreateModelAsync(graf, cancellationToken);
        if (createdGraf is null)
            return BadRequest("Graf can not be created");

        var grafResponse = mapper.Map<GrafResponse>(createdGraf);
        return CreatedAtAction(nameof(GetGrafById), new { grafResponse.Id }, grafResponse);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteGraf(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await grafsApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"Graf with id:{id} not found");
    }
}