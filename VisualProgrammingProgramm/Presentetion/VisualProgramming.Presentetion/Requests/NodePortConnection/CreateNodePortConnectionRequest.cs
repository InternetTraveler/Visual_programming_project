namespace VisualProgramming.Presentetion.Requests.NodePortConnection;

public record CreateNodePortConnectionRequest(Guid? nodeId, Guid? portId);