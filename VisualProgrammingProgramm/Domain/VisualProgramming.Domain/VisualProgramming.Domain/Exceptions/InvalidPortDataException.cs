using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Enum;

namespace VisualProgramming.Domain.Exceptions;

public class InvalidPortDataException(BaseNode? node, TypePort typePort, string description, string message)
    : Exception($"Invalid port data: {message}")
{
    public BaseNode? Node { get; } = node;
    public TypePort TypePort { get; } = typePort;
    public string Description { get; } = description;
    public string Message { get; } = $"Invalid port data: {message}";
}