using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Presentetion.Requests.ElementGraf;
using VisualProgramming.Presentetion.Responses.ElementGraf;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.Connection;
using VisualProgramming.WebHost.Responses.Port;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/elementgraf/[controller]")]
public class ElementGrafsController(IElementGrafAplicationService elementGrafsApplicationService,
                                    IPortAplicationService portsApplicationService,
                                    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ElementGrafResponse>))]
    public async Task<IActionResult> GetAllElementGrafs(CancellationToken cancellationToken)
    {
        var elements = await elementGrafsApplicationService.GetModelsAsync(cancellationToken);

        return Ok(mapper.Map<IEnumerable<ElementGrafResponse>>(elements));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ElementGrafResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetElementGrafById(Guid id, CancellationToken cancellationToken)
    {
        var element = await elementGrafsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (element is null)
            return NotFound($"ElementGraf with id:{id} not found");
        return Ok(mapper.Map<ElementGrafResponse>(element));
    }

    // создание элемента
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ElementGrafResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateElementGraf(CreateElementGrafRequest request, CancellationToken cancellationToken)
    {
        var element = mapper.Map<CreateElementGrafModel>(request);
        var createdElement = await elementGrafsApplicationService.CreateModelAsync(element, cancellationToken);
        if (createdElement is null)
            return BadRequest("ElementGraf can not be created");

        var elementResponse = mapper.Map<ElementGrafResponse>(createdElement);
        return CreatedAtAction(nameof(GetElementGrafById), new { elementResponse.Id }, elementResponse);
    }

    // просмотр всех соединений элемента графа
    [HttpGet("{id:guid}/connections")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ConnectionResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetElementGrafConnections(Guid id, CancellationToken cancellationToken)
    {
        var element = await elementGrafsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (element is null)
            return NotFound($"ElementGraf with id:{id} not found");

        var connections = await elementGrafsApplicationService.GetConnectionsAsync(id, cancellationToken);
        return Ok(mapper.Map<IEnumerable<ConnectionResponse>>(connections));
    }

    // просмотр портов элемента графа (порты узла, к которому привязан элемент)
    [HttpGet("{id:guid}/ports")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PortResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetElementGrafPorts(Guid id, CancellationToken cancellationToken)
    {
        var element = await elementGrafsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (element is null)
            return NotFound($"ElementGraf with id:{id} not found");

        var ports = await portsApplicationService.GetByNodeIdAsync(element.BaseNodeId, cancellationToken);
        return Ok(mapper.Map<IEnumerable<PortResponse>>(ports));
    }

    // удаление элемента перестраивает или удаляет граф
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteElementGraf(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await elementGrafsApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"ElementGraf with id:{id} not found");
    }
}