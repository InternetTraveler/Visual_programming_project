using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.Node;
using VisualProgramming.Presentetion.Requests.Node;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.Node;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/node/[controller]")]
public class NodesController(INodeAplicationService nodesApplicationService,
                             IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<NodeResponse>))]
    public async Task<IActionResult> GetAllNodes(CancellationToken cancellationToken)
    {
        var nodes = await nodesApplicationService.GetModelsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<NodeResponse>>(nodes));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NodeResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetNodeById(Guid id, CancellationToken cancellationToken)
    {
        var node = await nodesApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (node is null)
            return NotFound($"Node with id:{id} not found");
        return Ok(mapper.Map<NodeResponse>(node));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NodeResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateNode(CreateNodeRequest request, CancellationToken cancellationToken)
    {
        var node = mapper.Map<CreateNodeModel>(request);
        var createdNode = await nodesApplicationService.CreateModelAsync(node, cancellationToken);
        if (createdNode is null)
            return BadRequest("Node can not be created");

        var nodeResponse = mapper.Map<NodeResponse>(createdNode);
        return CreatedAtAction(nameof(GetNodeById), new { nodeResponse.Id }, nodeResponse);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteNode(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await nodesApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"Node with id:{id} not found");
    }
}