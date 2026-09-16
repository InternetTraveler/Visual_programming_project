
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

public interface INodePortConnectionAplicationService : IApplicationService<NodePortConnectionModul, CreateNodePortConnectionModul, Guid>
{

}
