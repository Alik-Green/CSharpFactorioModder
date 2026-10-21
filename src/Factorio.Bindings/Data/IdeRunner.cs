using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Factorio
{
    /// <summary>
    /// Invoked when a mod project is started from an IDE's Run button (<c>dotnet run</c>).
    /// Verifies the built Factorio mod zip and automatically copies it into the local Factorio
    /// <c>mods/</c> directory when present; pass <c>--launch</c> to also start Factorio.
    /// </summary>
    public static class IdeRunner
    {
        /// <summary>Deploys the built mod archive and optionally launches Factorio.</summary>
        public static int Run(string[] args)
        {
            var projectDir = FindProjectDirectory();
            if (projectDir == null)
            {
                Console.WriteLine("[Factorio C# Modder] Build complete.");
                return 0;
            }

            var manifestPath = Path.Combine(projectDir, "mod.json");
            var modName = Path.GetFileName(projectDir);
            var title = modName;
            var version = "0.1.0";

            try
            {
                using var doc = JsonDocument.Parse(File.ReadAllText(manifestPath));
                var root = doc.RootElement;
                if (root.TryGetProperty("name", out var n) && n.GetString() is { Length: > 0 } nv) modName = nv;
                if (root.TryGetProperty("title", out var t) && t.GetString() is { Length: > 0 } tv) title = tv;
                if (root.TryGetProperty("version", out var v) && v.GetString() is { Length: > 0 } vv) version = vv;
            }
            catch
            {
                // Keep fallback metadata if mod.json cannot be re-parsed.
            }

            var buildModsDir = Path.Combine(projectDir, "build", "mods");
            var expectedZip = Path.Combine(buildModsDir, $"{modName}_{version}.zip");
            string? zipPath = File.Exists(expectedZip)
                ? expectedZip
                : Directory.Exists(buildModsDir)
                    ? Directory.EnumerateFiles(buildModsDir, "*.zip")
                        .OrderByDescending(File.GetLastWriteTimeUtc)
                        .FirstOrDefault()
                    : null;

            Console.WriteLine($"[Factorio C# Modder] {title} v{version}");
            if (zipPath == null)
            {
                Console.WriteLine($"  Build directory: {buildModsDir}");
                return 0;
            }

            Console.WriteLine($"  Package : {zipPath}");

            var factorioModsDir = ResolveFactorioModsDirectory();
            if (factorioModsDir != null)
            {
                try
                {
                    Directory.CreateDirectory(factorioModsDir);
                    foreach (var oldZip in Directory.EnumerateFiles(factorioModsDir, $"{modName}_*.zip"))
                    {
                        if (!string.Equals(Path.GetFullPath(oldZip), Path.GetFullPath(zipPath), StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(Path.GetFileName(oldZip), Path.GetFileName(zipPath), StringComparison.OrdinalIgnoreCase))
                        {
                            File.Delete(oldZip);
                        }
                    }

                    var destination = Path.Combine(factorioModsDir, Path.GetFileName(zipPath));
                    File.Copy(zipPath, destination, overwrite: true);
                    Console.WriteLine($"  Deployed: {destination}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Note    : could not copy to '{factorioModsDir}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("  Tip     : set FACTORIO_MODS_DIR (or install Factorio) to auto-copy into Factorio's mods folder.");
            }

            var factorioExe = ResolveFactorioExecutable();
            var launch = args.Any(a => string.Equals(a, "--launch", StringComparison.OrdinalIgnoreCase)) ||
                         string.Equals(Environment.GetEnvironmentVariable("FACTORIO_LAUNCH"), "1", StringComparison.Ordinal);

            if (launch)
            {
                if (factorioExe == null)
                {
                    Console.Error.WriteLine("  Factorio executable was not found. Set FACTORIO_EXE=<path to factorio.exe>.");
                    return 1;
                }

                Console.WriteLine($"  Launching Factorio: {factorioExe}");
                Process.Start(new ProcessStartInfo(factorioExe) { UseShellExecute = true });
            }
            else if (factorioExe != null)
            {
                Console.WriteLine("  Ready   : start Factorio (or add `--launch` to Program Arguments in Rider to launch automatically).");
            }

            return 0;
        }

        private static string? FindProjectDirectory()
        {
            foreach (var start in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
            {
                if (string.IsNullOrWhiteSpace(start)) continue;
                var dir = new DirectoryInfo(start);
                while (dir != null)
                {
                    if (File.Exists(Path.Combine(dir.FullName, "mod.json")))
                        return dir.FullName;
                    dir = dir.Parent;
                }
            }

            return null;
        }

        private static string? ResolveFactorioModsDirectory()
        {
            var custom = Environment.GetEnvironmentVariable("FACTORIO_MODS_DIR");
            if (!string.IsNullOrWhiteSpace(custom)) return custom;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var factorioRoot = Path.Combine(appData, "Factorio");
                if (Directory.Exists(factorioRoot))
                    return Path.Combine(factorioRoot, "mods");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var factorioRoot = Path.Combine(home, "Library", "Application Support", "factorio");
                if (Directory.Exists(factorioRoot))
                    return Path.Combine(factorioRoot, "mods");
            }
            else
            {
                var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var factorioRoot = Path.Combine(home, ".factorio");
                if (Directory.Exists(factorioRoot))
                    return Path.Combine(factorioRoot, "mods");
            }

            return null;
        }

        private static string? ResolveFactorioExecutable()
        {
            var custom = Environment.GetEnvironmentVariable("FACTORIO_EXE");
            if (!string.IsNullOrWhiteSpace(custom) && File.Exists(custom)) return custom;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var candidates = new[]
                {
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam", "steamapps", "common", "Factorio", "bin", "x64", "factorio.exe"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Steam", "steamapps", "common", "Factorio", "bin", "x64", "factorio.exe"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Factorio", "bin", "x64", "factorio.exe"),
                };
                return candidates.FirstOrDefault(File.Exists);
            }

            return null;
        }
    }
}
