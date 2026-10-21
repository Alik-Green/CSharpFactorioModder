using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod plugin`: builds the Rider plugin from <c>rider-plugin/</c> into an installable zip.
///
/// IntelliJ-platform plugins are normally built with Gradle, which needs a working Gradle
/// distribution, a JDK and a 700 MB platform download before it can compile a 1,000-line plugin.
/// This command does the same three things Gradle would - compile against the platform API, package
/// the classes with <c>META-INF/plugin.xml</c>, zip it - using the JDK and the platform jars it can
/// find, and downloading only what is missing (once, into a cache).
/// </summary>
public static class PluginCommand
{
    /// <summary>The IntelliJ Platform the plugin is compiled against unless `--idea` says otherwise.</summary>
    public const string DefaultPlatform = "2024.2.5";

    /// <summary>The Java release the plugin is compiled for; plugins must target 17 since 2022.3.</summary>
    private const string JavaRelease = "17";

    /// <summary>Name of the jar inside the plugin zip.</summary>
    private const string JarName = "factorio-mod-rider.jar";

    public static int Run(ToolchainSettings settings, string[] args)
    {
        var source = Path.Combine(settings.Root, "rider-plugin");
        if (!Directory.Exists(source))
            throw new ToolException($"no rider-plugin folder in {settings.Root}");

        var pluginXml = Path.Combine(source, "src/main/resources/META-INF/plugin.xml");
        if (!File.Exists(pluginXml)) throw new ToolException("rider-plugin/src/main/resources/META-INF/plugin.xml is missing");

        var version = VersionOf(pluginXml);
        var cache = CacheDirectory();
        var offline = args.Contains("--offline");
        var platform = Value(args, "--idea") ?? DefaultPlatform;
        var output = Value(args, "--output") ?? Path.Combine(settings.BuildDirectory, "plugin");

        Console.WriteLine($"plugin     {version}");
        Console.WriteLine($"source     {source}");
        Console.WriteLine($"cache      {cache}");

        var javac = FindJavac(Value(args, "--jdk"), cache, offline);
        Console.WriteLine($"jdk        {Path.GetDirectoryName(Path.GetDirectoryName(javac))}");

        var classpath = PlatformJars(platform, cache, offline);
        Console.WriteLine($"platform   {classpath.Count} jar(s), {platform}");

        var classes = Path.Combine(settings.BuildDirectory, "obj", "rider-plugin");
        if (Directory.Exists(classes)) Directory.Delete(classes, true);
        Directory.CreateDirectory(classes);

        var sources = Directory.EnumerateFiles(Path.Combine(source, "src/main/java"), "*.java", SearchOption.AllDirectories)
            .OrderBy(f => f, StringComparer.Ordinal)
            .ToList();
        if (sources.Count == 0) throw new ToolException("no *.java under rider-plugin/src/main/java");

        Compile(javac, classpath, sources, classes, settings);
        Console.WriteLine($"compiled   {sources.Count} file(s)");

        // Outside the classes folder: the jar would otherwise be packed into itself. Removed
        // first, so a jar a previous run left open cannot make this one fail.
        var jar = Path.Combine(settings.BuildDirectory, "obj", JarName);
        if (File.Exists(jar)) File.Delete(jar);
        WriteJar(jar, classes, Path.Combine(source, "src/main/resources"));
        Console.WriteLine($"jar        {JarName} ({new FileInfo(jar).Length / 1024} KB)");

        Directory.CreateDirectory(output);
        var zip = Path.Combine(output, $"FactorioCSharpMods-{version}.zip");
        if (File.Exists(zip)) File.Delete(zip);

        // The IDE expects one folder at the top of the zip, holding lib/.
        var folder = $"FactorioCSharpMods";
        using (var archive = ZipFile.Open(zip, ZipArchiveMode.Create))
        {
            using (var entry = archive.CreateEntry($"{folder}/lib/{JarName}", CompressionLevel.Optimal).Open())
            using (var file = File.OpenRead(jar))
            {
                file.CopyTo(entry);
            }
        }

        Console.WriteLine($"zip        {zip}");

        // Also keep a copy at plugin/ in the repository, which is the one that is easy to find
        // without knowing where the build wrote it.
        var published = Path.Combine(settings.Root, "plugin", Path.GetFileName(zip));
        Directory.CreateDirectory(Path.GetDirectoryName(published)!);
        File.Copy(zip, published, true);
        Console.WriteLine($"copy       {published}");

        Console.WriteLine();
        Console.WriteLine("install    Settings -> Plugins -> Install Plugin from Disk..., then restart the IDE");
        Console.WriteLine("           (the plugin needs Rider 2024.2 or newer, or any IDE on the same platform)");
        return 0;
    }

    /// <summary>The plugin's own version, read from plugin.xml so it is written down once.</summary>
    public static string VersionOf(string pluginXml)
    {
        var text = File.ReadAllText(pluginXml);
        var match = Regex.Match(text, "<version>([^<]+)</version>");
        return match.Success ? match.Groups[1].Value.Trim() : "0.0.0";
    }

    /// <summary>
    /// Where downloads and the extracted platform live: the user's cache directory, never the
    /// repository, because it is hundreds of megabytes the repository does not need.
    /// </summary>
    public static string CacheDirectory() =>
        Path.Combine(
            Environment.GetFolderPath(OperatingSystem.IsWindows()
                ? Environment.SpecialFolder.LocalApplicationData
                : Environment.SpecialFolder.UserProfile),
            OperatingSystem.IsWindows() ? "factorio-mod" : ".cache/factorio-mod");

    // ------------------------------------------------------------------ javac

    /// <summary>Finds a JDK's javac, downloading one when there is none.</summary>
    private static string FindJavac(string? explicitPath, string cache, bool offline)
    {
        foreach (var candidate in new[]
                 {
                     explicitPath,
                     Environment.GetEnvironmentVariable("FACTORIO_MOD_JDK"),
                     Environment.GetEnvironmentVariable("JAVA_HOME"),
                     Directory.Exists(Path.Combine(cache, "jdk-17")) ? Path.Combine(cache, "jdk-17") : null,
                 })
        {
            if (string.IsNullOrWhiteSpace(candidate)) continue;
            var javac = JavacIn(candidate);
            if (javac != null) return javac;
        }

        var onPath = Where("javac");
        if (onPath != null) return onPath;

        if (offline)
            throw new ToolException(
                "no JDK found (set JAVA_HOME or pass --jdk DIR) and --offline was given, so one " +
                "cannot be downloaded. Rider ships one: point --jdk at Rider's jbr folder.");

        var target = Path.Combine(cache, "jdk-17");
        DownloadJdk(target);
        return JavacIn(target) ?? throw new ToolException("the downloaded JDK has no bin/javac");
    }

    /// <summary>javac inside a JDK folder, or null when the folder is not a JDK.</summary>
    private static string? JavacIn(string directory)
    {
        foreach (var name in new[] { "javac", "javac.exe" })
        {
            var path = Path.Combine(directory, "bin", name);
            if (File.Exists(path)) return path;
        }

        // `--jdk` may have been given the bin folder itself.
        foreach (var name in new[] { "javac", "javac.exe" })
        {
            var path = Path.Combine(directory, name);
            if (File.Exists(path)) return path;
        }

        return null;
    }

    /// <summary>Downloads and unpacks Temurin 17 into <paramref name="target"/>.</summary>
    private static void DownloadJdk(string target)
    {
        var os = OperatingSystem.IsWindows() ? "windows" : OperatingSystem.IsMacOS() ? "mac" : "linux";
        var arch = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture ==
                   System.Runtime.InteropServices.Architecture.Arm64
            ? "aarch64"
            : "x64";
        var url = $"https://api.adoptium.net/v3/binary/latest/17/ga/{os}/{arch}/jdk/hotspot/normal/eclipse";

        var archive = Path.Combine(Path.GetTempPath(), os == "windows" ? "temurin17.zip" : "temurin17.tar.gz");
        Console.WriteLine($"downloading a JDK from {url}");
        Download(url, archive);

        Directory.CreateDirectory(target);
        if (os == "windows")
        {
            ZipFile.ExtractToDirectory(archive, target + ".tmp", true);
        }
        else
        {
            Directory.CreateDirectory(target + ".tmp");
            var tar = Process.Start(new ProcessStartInfo("tar", $"xzf \"{archive}\" -C \"{target}.tmp\" --strip-components=1")
            {
                RedirectStandardError = true,
                UseShellExecute = false,
            })!;
            var error = tar.StandardError.ReadToEnd();
            tar.WaitForExit();
            if (tar.ExitCode != 0) throw new ToolException("tar failed: " + error.Trim());
        }

        // Move the one top-level folder the archive has into place.
        var unpacked = Directory.EnumerateDirectories(target + ".tmp").ToList();
        var root = os == "windows" && unpacked.Count == 1 ? unpacked[0] : target + ".tmp";
        foreach (var file in Directory.EnumerateFileSystemEntries(root))
        {
            var name = Path.GetFileName(file);
            if (name is "bin" or "lib" or "conf" or "include" or "jmods" or "legal" or "release")
                Move(file, Path.Combine(target, name));
        }

        TryDelete(target + ".tmp");
        TryDelete(archive);
        Console.WriteLine($"jdk        {target}");
    }

    // ------------------------------------------------------------------ platform

    /// <summary>
    /// The platform jars to compile against: from a directory the user pointed at, or from the
    /// cached copy of the IDE archive.
    /// </summary>
    private static List<string> PlatformJars(string platform, string cache, bool offline)
    {
        if (Directory.Exists(platform))
            return Jars(platform);

        // --idea may name an already-extracted archive.
        var local = Path.Combine(cache, "idea-" + platform);
        if (Directory.Exists(local) && Directory.EnumerateFiles(local, "*.jar").Any())
            return Jars(local);

        if (File.Exists(platform) && platform.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            return ExtractPlatform(platform, local);

        var archivePath = Path.Combine(cache, $"ideaIC-{platform}.zip");
        if (!File.Exists(archivePath))
        {
            if (offline)
                throw new ToolException(
                    $"the IntelliJ Platform {platform} is not in the cache ({archivePath}) and --offline was " +
                    "given. Run once without --offline, or pass --idea <an extracted IDE folder>.");

            var url = "https://cache-redirector.jetbrains.com/intellij-repository/releases/" +
                      $"com/jetbrains/intellij/idea/ideaIC/{platform}/ideaIC-{platform}.zip";
            Console.WriteLine($"downloading the platform from {url}");
            Download(url, archivePath);
        }

        return ExtractPlatform(archivePath, local);
    }

    /// <summary>Extracts every top-level lib/*.jar out of an IDE archive, once.</summary>
    private static List<string> ExtractPlatform(string archivePath, string target)
    {
        if (Directory.Exists(target) && Directory.EnumerateFiles(target, "*.jar").Any())
            return Jars(target);

        Directory.CreateDirectory(target);
        using var archive = ZipFile.OpenRead(archivePath);
        foreach (var entry in archive.Entries)
        {
            if (!entry.FullName.StartsWith("lib/", StringComparison.Ordinal)) continue;
            if (!entry.Name.EndsWith(".jar", StringComparison.OrdinalIgnoreCase)) continue;

            var destination = Path.Combine(target, entry.Name);
            if (!File.Exists(destination)) entry.ExtractToFile(destination);
        }

        var jars = Jars(target);
        if (jars.Count == 0) throw new ToolException($"no jars found in {archivePath}");
        Console.WriteLine($"platform   extracted {jars.Count} jar(s) to {target}");
        return jars;
    }

    private static List<string> Jars(string directory) =>
        Directory.EnumerateFiles(directory, "*.jar").OrderBy(f => f, StringComparer.Ordinal).ToList();

    // ------------------------------------------------------------------ compile + package

    /// <summary>Runs javac over the plugin's sources.</summary>
    private static void Compile(string javac, List<string> classpath, List<string> sources, string classes, ToolchainSettings settings)
    {
        var arguments = new List<string>
        {
            $"--release", JavaRelease,
            "-encoding", "UTF-8",
            "-classpath", string.Join(Path.PathSeparator, classpath),
            "-d", classes,
        };
        arguments.AddRange(sources);

        // The same trick the C# compile uses: an argument file keeps the command line short enough
        // for Windows, where 40 jars plus 10 sources already approaches the limit.
        var argumentFile = Path.Combine(classes, "javac.args");
        File.WriteAllLines(argumentFile, arguments.Select(Quote));

        var run = Process.Start(new ProcessStartInfo(javac, "@" + Quote(argumentFile))
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = settings.Root,
        })!;

        var stdout = run.StandardOutput.ReadToEnd();
        var stderr = run.StandardError.ReadToEnd();
        run.WaitForExit();

        if (!string.IsNullOrWhiteSpace(stdout)) Console.WriteLine(stdout.TrimEnd());
        if (run.ExitCode != 0)
            throw new ToolException("javac failed:\n" + string.Join("\n",
                (stderr + "\n" + stdout).Split('\n').Where(l => l.Contains("error")).Take(20)));
    }

    /// <summary>Wraps an argument in quotes when it needs them, for an argument file.</summary>
    private static string Quote(string value) =>
        value.Contains(' ') || value.Contains('"') ? "\"" + value.Replace("\"", "\\\"") + "\"" : value;

    /// <summary>
    /// Writes the plugin jar: the compiled classes plus the resources (plugin.xml and icons), which
    /// is exactly what the platform reads from <c>lib/*.jar</c>.
    /// </summary>
    private static void WriteJar(string jar, string classes, string resources)
    {
        using var archive = ZipFile.Open(jar, ZipArchiveMode.Create);
        var entries = new HashSet<string>(StringComparer.Ordinal);

        foreach (var file in Directory.EnumerateFiles(classes, "*", SearchOption.AllDirectories))
        {
            var name = Path.GetRelativePath(classes, file).Replace('\\', '/');
            // The javac argument file is an input, not part of the plugin.
            if (name.EndsWith(".args", StringComparison.Ordinal)) continue;
            archive.CreateEntryFromFile(file, name, CompressionLevel.Optimal);
            entries.Add(name);
        }

        foreach (var file in Directory.EnumerateFiles(resources, "*", SearchOption.AllDirectories))
        {
            var name = Path.GetRelativePath(resources, file).Replace('\\', '/');
            if (!entries.Add(name)) continue;
            archive.CreateEntryFromFile(file, name, CompressionLevel.Optimal);
        }
    }

    // ------------------------------------------------------------------ helpers

    /// <summary>Downloads a URL to a file, following redirects.</summary>
    private static void Download(string url, string destination)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
        using var client = new HttpClient { Timeout = TimeSpan.FromMinutes(30) };
        using var response = client.Send(new HttpRequestMessage(HttpMethod.Get, url), HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
            throw new ToolException($"download failed: HTTP {(int)response.StatusCode} for {url}");

        using var stream = response.Content.ReadAsStream();
        using var file = File.Create(destination);
        stream.CopyTo(file);
        Console.WriteLine($"           {new FileInfo(destination).Length / (1024 * 1024)} MB");
    }

    /// <summary>`where`/`which` for one program.</summary>
    private static string? Where(string program)
    {
        var path = Environment.GetEnvironmentVariable("PATH");
        if (path == null) return null;

        foreach (var directory in path.Split(Path.PathSeparator))
        {
            if (string.IsNullOrWhiteSpace(directory)) continue;
            foreach (var name in new[] { program, program + ".exe", program + ".cmd", program + ".bat" })
            {
                var candidate = Path.Combine(directory, name);
                if (File.Exists(candidate)) return candidate;
            }
        }

        return null;
    }

    /// <summary>Moves a file or directory, replacing what is there.</summary>
    private static void Move(string from, string to)
    {
        if (Directory.Exists(to)) Directory.Delete(to, true);
        else if (File.Exists(to)) File.Delete(to);

        if (Directory.Exists(from)) Directory.Move(from, to);
        else File.Move(from, to);
    }

    /// <summary>Deletes a file or directory, ignoring failures: it is a cache.</summary>
    private static void TryDelete(string path)
    {
        try
        {
            if (Directory.Exists(path)) Directory.Delete(path, true);
            else if (File.Exists(path)) File.Delete(path);
        }
        catch (IOException)
        {
            // Leaving a temporary file behind is not worth failing the build over.
        }
    }

    private static string? Value(string[] args, string name) => CommandLine.Value(args, name);
}
