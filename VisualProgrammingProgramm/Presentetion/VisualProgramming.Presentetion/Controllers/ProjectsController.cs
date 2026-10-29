using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VisualProgramming.Moduls.Project;
using VisualProgramming.Presentetion.Requests.Project;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.WebHost.Responses.Graf;
using VisualProgramming.WebHost.Responses.Project;

namespace VisualProgramming.WebHost.Controllers;

[ApiController]
[Route("api/project/[controller]")]
public class ProjectsController(IProjectAplicationService projectsApplicationService,
                                IGrafAplicationService grafsApplicationService,
                                IMapper mapper) : ControllerBase
{
    // просмотр всех проектов
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectResponse>))]
    public async Task<IActionResult> GetAllProjects(CancellationToken cancellationToken)
    {
        var projects = await projectsApplicationService.GetModelsAsync(cancellationToken);
        return Ok(mapper.Map<IEnumerable<ProjectResponse>>(projects));
    }

    // просмотр одного проекта
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetProjectById(Guid id, CancellationToken cancellationToken)
    {
        var project = await projectsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (project is null)
            return NotFound($"Project with id:{id} not found");
        return Ok(mapper.Map<ProjectResponse>(project));
    }

    // при выборе проекта показываются все его графы
    [HttpGet("{id:guid}/grafs")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GrafResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetProjectGrafs(Guid id, CancellationToken cancellationToken)
    {
        var project = await projectsApplicationService.GetModelByIdAsync(id, cancellationToken);
        if (project is null)
            return NotFound($"Project with id:{id} not found");

        var grafs = await grafsApplicationService.GetModelsAsync(cancellationToken);
        var projectGrafs = grafs.Where(g => g.projectId == id);
        return Ok(mapper.Map<IEnumerable<GrafResponse>>(projectGrafs));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> CreateProject(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<CreateProjectModel>(request);
        var createdProject = await projectsApplicationService.CreateModelAsync(project, cancellationToken);
        if (createdProject is null)
            return BadRequest("Project can not be created");

        var projectResponse = mapper.Map<ProjectResponse>(createdProject);
        return CreatedAtAction(nameof(GetProjectById), new { projectResponse.Id }, projectResponse);
    }

    // удаление проекта
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await projectsApplicationService.DeleteModelAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound($"Project with id:{id} not found");
    }
}