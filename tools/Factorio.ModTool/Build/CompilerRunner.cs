namespace Factorio.ModTool.Build;

/// <summary>
/// Invokes the pinned CSharp.lua compiler via a single-line relative response file so a 1,500-file
/// binding list never hits OS command-line length limits or path-space splitting issues.
/// </summary>
public static class CompilerRunner
{
    public sealed record Result(string OutputDirectory, IReadOnlyList<string> LuaFiles, string LogPath);

    /// <summary>
    /// Writes the single-line response file consumed by <c>CSharp.lua.Launcher @file</c>, keeping
    /// all repository paths relative to <see cref="ToolchainSettings.Root"/> with forward slashes.
    /// </summary>
    public static string WriteArgumentsFile(
        ToolchainSettings settings,
        IReadOnlyList<string> files,
        string output,
        bool classicLua)
    {
        var responseDirectory = Directory.GetParent(Path.GetFullPath(output))?.FullName ?? settings.BuildDirectory;
        Directory.CreateDirectory(responseDirectory);
        var responsePath = Path.Combine(responseDirectory, "csharp-lua.rsp");

        string Relative(string target) =>
            Path.GetRelativePath(settings.Root, target).Replace('\\', '/');

        var sources = string.Join(";", files.Select(Relative));
        var destination = Relative(output);
        var line = $"-s {sources} -d {destination} -noconcurrent" + (classicLua ? " -c" : "");

        File.WriteAllText(responsePath, line + "\n");
        return responsePath;
    }

    /// <summary>Compiles C# sources to per-type Lua modules using CSharp.lua.</summary>
    public static Result Compile(
        ToolchainSettings settings,
        string modName,
        IReadOnlyList<string> files,
        bool classicLua)
    {
        var dotnet = Shell.Dotnet(settings);
        var compiler = Shell.Compiler(settings);

        var luaOutDir = Path.Combine(settings.BuildDirectory, "obj", modName, "lua");
        if (Directory.Exists(luaOutDir)) Directory.Delete(luaOutDir, recursive: true);
        Directory.CreateDirectory(luaOutDir);

        var responsePath = WriteArgumentsFile(settings, files, luaOutDir, classicLua);
        var run = Shell.Run(dotnet, new[] { compiler, "@" + responsePath }, workingDirectory: settings.Root);

        var logsDir = Path.Combine(settings.BuildDirectory, "logs");
        Directory.CreateDirectory(logsDir);
        var logPath = Path.Combine(logsDir, modName + ".compiler.log");
        File.WriteAllText(logPath, run.StdOut + (run.StdErr.Length > 0 ? "\n" + run.StdErr : ""));

        if (run.ExitCode != 0)
        {
            throw new ToolException(
                $"CSharp.lua failed (full log: {logPath}):\n" +
                ModBuilder.Tail((run.StdOut + "\n" + run.StdErr).Trim(), 25));
        }

        var emitted = Directory.EnumerateFiles(luaOutDir, "*.lua", SearchOption.AllDirectories)
            .OrderBy(p => p, StringComparer.Ordinal)
            .ToList();

        return new Result(luaOutDir, emitted, logPath);
    }
}
