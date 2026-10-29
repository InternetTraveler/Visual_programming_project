using AutoMapper;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Moduls.Connection;
using VisualProgramming.Moduls.ElementGraf;
using VisualProgramming.Moduls.Graf;
using VisualProgramming.Moduls.Modul;
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Moduls.Port;
using VisualProgramming.Moduls.Node;
using VisualProgramming.Moduls.Project;

namespace VisualProgramming.Servises.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile() {
        CreateMap<Connection, ConnectionModel>();

        CreateMap<ElementGraf, ElementGrafModel>()
            .ConstructUsing(src => new ElementGrafModel(
                src.Id,
                src.Node == null ? Guid.Empty : src.Node.Id,
                src.LevelLevelOfDepthOperation.Value,
                src.IsModul,
                src.ParentGraf == null ? Guid.Empty : src.ParentGraf.Id,
                src.PositionX,
                src.PositionY));

        CreateMap<Graf, GrafModel>()
            .ForMember(dest => dest.projectId,
               opt => opt.MapFrom(src => src.Project.Id));

        CreateMap<Modul, ModulModel>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.Value));

        CreateMap<Node, NodeModel>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.typeOperation,
                        opt => opt.MapFrom(src => (int)src.TypeOperation));

        CreateMap<NodePortConnection, NodePortConnectionModel>();

        CreateMap<Port, PortModel>()
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.typePort,
                        opt => opt.MapFrom(src => (int)src.TypePort));

        CreateMap<Project, ProjectModel>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.Value));
    }
}
