namespace Factorio.ModTool.Commands;

/// <summary>
/// The little bit of argument parsing every command needs.
///
/// The important part is <see cref="Positional"/>: a directory that follows an option that takes a
/// value (`--output ~/.factorio/mods`) is that option's value, not another mod to build. Commands
/// that take both a list of paths and options with values must use it.
/// </summary>
public static class CommandLine
{
    /// <summary>Arguments that are not switches and not the value of an option.</summary>
    /// <param name="args">The command's arguments, without the command name.</param>
    /// <param name="valueOptions">Options that take the next argument as their value.</param>
    /// <returns>The positional arguments, in order.</returns>
    public static List<string> Positional(string[] args, params string[] valueOptions)
    {
        var positional = new List<string>();
        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            if (valueOptions.Contains(arg) && i + 1 < args.Length)
            {
                i++;
                continue;
            }

            if (arg.StartsWith("-", StringComparison.Ordinal)) continue;
            positional.Add(arg);
        }

        return positional;
    }

    /// <summary>The value of `--name value`, or null.</summary>
    /// <param name="args">Arguments to scan.</param>
    /// <param name="name">The option name, with its leading dashes.</param>
    /// <returns>The value, or null when the option is absent.</returns>
    public static string? Value(string[] args, string name)
    {
        for (var i = 0; i < args.Length - 1; i++)
            if (args[i] == name) return args[i + 1];
        return null;
    }

    /// <summary>A path for the console: relative when inside the root, absolute when outside it.</summary>
    /// <param name="root">Repository root.</param>
    /// <param name="path">Path to print.</param>
    /// <returns>The path to show.</returns>
    public static string Display(string root, string path)
    {
        var relative = Path.GetRelativePath(root, path);
        return relative.StartsWith("..", StringComparison.Ordinal) || Path.IsPathRooted(relative) ? path : relative;
    }
}
