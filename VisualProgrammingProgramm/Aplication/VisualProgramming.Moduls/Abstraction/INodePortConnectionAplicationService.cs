
using VisualProgramming.Moduls.NodePortConnection;
using VisualProgramming.Servises.Abstraction.Base;

namespace VisualProgramming.Servises.Abstraction;

internal interface INodePortConnectionAplicationService : IApplicationService<NodePortConnectionModul, CreateNodePortConnectionModul, Guid>
{

}
