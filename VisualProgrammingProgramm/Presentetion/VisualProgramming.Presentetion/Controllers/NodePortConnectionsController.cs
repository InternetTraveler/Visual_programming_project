using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Presentetion.Requests.NodePortConnection;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.NodePortConnection;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/nodeportconnection/[controller]")]
public class NodePortConnectionsController(INodePortConnectionAplicationService connectionsApplicationService,
                                           IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NodePortConnectionResponse>))]
    public async Task<IActionResult> GetAllNodePortConnections(CancellationToken cancellationToken)
    {
        var connections = await connectionsApplicationService.GetModelsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<NodePortConnectionResponse>>(connections));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NodePortConnectionResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetNodePortConnectionById(Guid id, CancellationToken cancellationToken)
    {
        var connection = await connectionsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (connection is null)
            return NotFound($"NodePortConnection with id:{id} not found");
        return Ok(mapper.Map<NodePortConnectionResponse>(connection));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NodePortConnectionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateNodePortConnection(CreateNodePortConnectionRequest request, CancellationToken cancellationToken)
    {
        var connection = mapper.Map<CreateNodePortConnectionModel>(request);
        var createdConnection = await connectionsApplicationService.CreateModelAsync(connection, cancellationToken);
        if (createdConnection is null)
            return BadRequest("NodePortConnection can not be created");

        var connectionResponse = mapper.Map<NodePortConnectionResponse>(createdConnection);
        return CreatedAtAction(nameof(GetNodePortConnectionById), new { connectionResponse.Id }, connectionResponse);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteNodePortConnection(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await connectionsApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"NodePortConnection with id:{id} not found");
    }
}