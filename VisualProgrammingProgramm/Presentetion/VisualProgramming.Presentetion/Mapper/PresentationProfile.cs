using AutoMapper;
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Moduls.Node;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Moduls.Port;
using VisualProgramming.Moduls.Project;
using VisualProgramming.Presentetion.Requests.ElementGraf;
using VisualProgramming.Presentetion.Requests.Graf;
using VisualProgramming.Presentetion.Requests.Modul;
using VisualProgramming.Presentetion.Requests.Node;
using VisualProgramming.Presentetion.Requests.NodePortConnection;
using VisualProgramming.Presentetion.Requests.Port;
using VisualProgramming.Presentetion.Requests.Project;
using VisualProgramming.Presentetion.Responses.ElementGraf;
using VisualProgramming.Presentetion.Responses.Moduls;
using VisualProgramming.Presentetion.Responses.Project;
using VisualProgramming.WebHost.Responses.Graf;
using VisualProgramming.WebHost.Responses.Modul;
using VisualProgramming.WebHost.Responses.Node;
using VisualProgramming.WebHost.Responses.NodePortConnection;
using VisualProgramming.WebHost.Responses.Port;
using VisualProgramming.WebHost.Responses.Project;
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Presentetion.Requests.Connection;
using VisualProgramming.WebHost.Responses.Connection;

namespace VisualProgramming.Presentetion.Mapper;

public class PresentationProfile : Profile
{
    public PresentationProfile()
    {
        // ── Connection ──────────────────────────────────────────────
        CreateMap<ConnectionModel, ConnectionResponse>();
        CreateMap<CreateConnectionRequest, CreateConnectionModel>();
        CreateMap<CreateConnectionModel, ConnectionResponse>();
        CreateMap<UpdateConnectionRequest, ConnectionModel>();

        // ── ElementGraf ─────────────────────────────────────────────
        CreateMap<ElementGrafModel, ElementGrafResponse>()
            .ConstructUsing(src => new ElementGrafResponse(
                src.Id,
                src.BaseNodeId,
                src.LevelLevelOfDepthOperation,
                src.IsModul,
                src.GrafId,
                src.PositionX,
                src.PositionY));


        CreateMap<CreateElementGrafRequest, CreateElementGrafModel>();
        CreateMap<UpdateElementGrafRequest, ElementGrafModel>();

        // ── Graf ────────────────────────────────────────────────────
        CreateMap<GrafModel, GrafResponse>();
        CreateMap<CreateGrafRequest, CreateGrafModel>();
        CreateMap<UpdateGrafRequest, GrafModel>();

        // ── Modul ───────────────────────────────────────────────────
        CreateMap<ModulModel, ModulResponse>();
        CreateMap<ModulModel, ModulDetailsResponse>();
        CreateMap<CreateModulRequest, CreateModulModel>();
        CreateMap<CreateModulModel, ModulResponse>();
        CreateMap<UpdateModulRequest, ModulModel>();

        // ── Node ────────────────────────────────────────────────────
        CreateMap<NodeModel, NodeResponse>();
        CreateMap<CreateNodeRequest, CreateNodeModel>();
        CreateMap<CreateNodeModel, NodeResponse>();
        CreateMap<UpdateNodeRequest, NodeModel>();

        // ── NodePortConnection ──────────────────────────────────────
        CreateMap<NodePortConnectionModel, NodePortConnectionResponse>();
        CreateMap<CreateNodePortConnectionRequest, CreateNodePortConnectionModel>();
        CreateMap<CreateNodePortConnectionModel, NodePortConnectionResponse>();
        CreateMap<UpdateNodePortConnectionRequest, NodePortConnectionModel>();

        // ── Port ────────────────────────────────────────────────────
        CreateMap<PortModel, PortResponse>();
        CreateMap<CreatePortRequest, CreatePortModel>();
        CreateMap<CreatePortModel, PortResponse>();
        CreateMap<UpdatePortRequest, PortModel>();

        // ── Project ─────────────────────────────────────────────────
        CreateMap<ProjectModel, ProjectResponse>();
        CreateMap<ProjectModel, ProjectDetailsResponse>();
        CreateMap<CreateProjectRequest, CreateProjectModel>();
        CreateMap<CreateProjectModel, ProjectResponse>();
        CreateMap<UpdateProjectRequest, ProjectModel>();
    }
}