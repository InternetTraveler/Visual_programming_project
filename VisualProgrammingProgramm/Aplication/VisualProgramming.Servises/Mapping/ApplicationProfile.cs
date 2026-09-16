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
        CreateMap<Connection, ConnectionModul>();

        CreateMap<ElementGraf, ElementGrafModul>()
            .ForMember(dest => dest.levelLevelOfDepthOperation, 
                        opt => opt.MapFrom(src => src.LevelLevelOfDepthOperation.Value));

        CreateMap<Graf, GrafModul>();

        CreateMap<Modul, ModulModul>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.Value));

        CreateMap<Node, NodeModul>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.typeOperation,
                        opt => opt.MapFrom(src => (int)src.TypeOperation));

        CreateMap<NodePortConnection, NodePortConnectionModul>();

        CreateMap<Port, PortModul>()
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.typePort,
                        opt => opt.MapFrom(src => (int)src.TypePort));

        CreateMap<Project, ProjectModul>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name.Value));
    }
}
