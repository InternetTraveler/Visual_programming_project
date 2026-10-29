using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Presentetion.Requests.Connection;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.Connection;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/connection/[controller]")]
public class ConnectionsController(IConnectionAplicationService connectionsApplicationService,
                                    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ConnectionResponse>))]
    public async Task<IActionResult> GetAllConnections(CancellationToken cancellationToken)
    {
        var connections = await connectionsApplicationService.GetModelsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<ConnectionResponse>>(connections));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConnectionResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetConnectionById(Guid id, CancellationToken cancellationToken)
    {
        var connection = await connectionsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (connection is null)
            return NotFound($"Connection with id:{id} not found");
        return Ok(mapper.Map<ConnectionResponse>(connection));
    }

    // соединение элементов (соединение создаётся и объединяет графы)
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ConnectionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateConnection(CreateConnectionRequest request, CancellationToken cancellationToken)
    {
        var connection = mapper.Map<CreateConnectionModel>(request);
        var createdConnection = await connectionsApplicationService.CreateModelAsync(connection, cancellationToken);
        if (createdConnection is null)
            return BadRequest("Connection can not be created");

        var connectionResponse = mapper.Map<ConnectionResponse>(createdConnection);
        return CreatedAtAction(nameof(GetConnectionById), new { connectionResponse.Id }, connectionResponse);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteConnection(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await connectionsApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"Connection with id:{id} not found");
    }
}