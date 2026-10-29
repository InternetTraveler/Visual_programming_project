using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Presentetion.Requests.Modul;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.Graf;
using VisualProgramming.WebHost.Responses.Modul;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/modul/[controller]")]
public class ModulsController(IModulAplicationService modulsApplicationService,
                              IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModulResponse>))]
    public async Task<IActionResult> GetAllModuls(CancellationToken cancellationToken)
    {
        var moduls = await modulsApplicationService.GetModelsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<ModulResponse>>(moduls));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModulResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetModulById(Guid id, CancellationToken cancellationToken)
    {
        var modul = await modulsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (modul is null)
            return NotFound($"Modul with id:{id} not found");
        return Ok(mapper.Map<ModulResponse>(modul));
    }

    // п.5: просмотр графа модуля
    [HttpGet("{id:guid}/graf")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GrafResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetModulGraf(Guid id, CancellationToken cancellationToken)
    {
        var modul = await modulsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (modul is null)
            return NotFound($"Modul with id:{id} not found");

        var graf = await modulsApplicationService.GetGrafOperationAsync(id, cancellationToken);
        if (graf is null)
            return NotFound($"Graf for modul with id:{id} not found");

        return Ok(mapper.Map<GrafResponse>(graf));
    }

    // п.2/10: создание модуля перепривязывает граф к проекту модуля и наследует граничные порты
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ModulResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateModul(CreateModulRequest request, CancellationToken cancellationToken)
    {
        var modul = mapper.Map<CreateModulModel>(request);
        var createdModul = await modulsApplicationService.CreateModelAsync(modul, cancellationToken);
        if (createdModul is null)
            return BadRequest("Modul can not be created");

        var modulResponse = mapper.Map<ModulResponse>(createdModul);
        return CreatedAtAction(nameof(GetModulById), new { modulResponse.Id }, modulResponse);
    }

    // п.7: удаление модуля каскадно удаляет граф, элементы, соединения и связи модуль-порт
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteModul(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await modulsApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"Modul with id:{id} not found");
    }
}