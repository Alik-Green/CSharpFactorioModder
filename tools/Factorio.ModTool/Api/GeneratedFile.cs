namespace Factorio.ModTool.Api;

/// <summary>One file produced by an emitter. Paths are relative and use '/' as separator.</summary>
public sealed class GeneratedFile
{
    public string Path { get; init; } = "";
    public string Content { get; init; } = "";
    public string Kind { get; init; } = "code";   // code | report | data
}

public sealed class EmitResult
{
    public List<GeneratedFile> Files { get; } = new();
    public List<Diagnostic> Diagnostics { get; } = new();
}
