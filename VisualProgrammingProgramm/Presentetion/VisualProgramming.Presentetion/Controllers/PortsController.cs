using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.Port;
using VisualProgramming.Presentetion.Requests.Port;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.Port;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/port/[controller]")]
public class PortsController(IPortAplicationService portsApplicationService,
                             IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PortResponse>))]
    public async Task<IActionResult> GetAllPorts(CancellationToken cancellationToken)
    {
        var ports = await portsApplicationService.GetModelsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<PortResponse>>(ports));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetPortById(Guid id, CancellationToken cancellationToken)
    {
        var port = await portsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (port is null)
            return NotFound($"Port with id:{id} not found");
        return Ok(mapper.Map<PortResponse>(port));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PortResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreatePort(CreatePortRequest request, CancellationToken cancellationToken)
    {
        var port = mapper.Map<CreatePortModel>(request);
        var createdPort = await portsApplicationService.CreateModelAsync(port, cancellationToken);
        if (createdPort is null)
            return BadRequest("Port can not be created");

        var portResponse = mapper.Map<PortResponse>(createdPort);
        return CreatedAtAction(nameof(GetPortById), new { portResponse.Id }, portResponse);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeletePort(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await portsApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"Port with id:{id} not found");
    }
}