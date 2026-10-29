namespace VisualProgramming.Presentetion.Requests.NodePortConnection;

public record UpdateNodePortConnectionRequest(Guid Id, Guid? NodeId, Guid? PortId);