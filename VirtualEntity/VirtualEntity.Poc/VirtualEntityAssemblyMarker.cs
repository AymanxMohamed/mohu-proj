using System.Reflection;

namespace VirtualEntity.Poc;

public static class VirtualEntityAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(VirtualEntityAssemblyMarker).Assembly;
}