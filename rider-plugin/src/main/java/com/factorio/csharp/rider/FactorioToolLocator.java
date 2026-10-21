package com.factorio.csharp.rider;

import com.intellij.openapi.project.Project;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.Locale;

/**
 * Finds {@code factorio-mod}.
 *
 * <p>Written for the common case - Rider opened on a mod inside this repository, or on the
 * repository itself - so the plugin works with no configuration at all. Everything else is a
 * fallback, in the order a user would try it:</p>
 *
 * <ol>
 *   <li>the path in the settings (either settings option);</li>
 *   <li>{@code FACTORIO_MOD_TOOL} (a command) or {@code FACTORIO_MOD_REPO} (a repository root);</li>
 *   <li>a {@code config/toolchain.json} in the open project or above it - that file is where the
 *       repository says what its toolchain is, so finding it means having found the repository;</li>
 *   <li>{@code factorio-mod} on {@code PATH}.</li>
 * </ol>
 */
public final class FactorioToolLocator {

    private FactorioToolLocator() {
    }

    /** The marker file every factorio-csharp checkout has at its root. */
    private static final String REPOSITORY_MARKER = "config/toolchain.json";

    /**
     * Resolves the tool, or null when it cannot be found.
     *
     * @param project The open project, used to start the search from it; may be null.
     * @return The command, or null.
     */
    public static @Nullable ToolCommand locate(@Nullable Project project) {
        String configured = FactorioSettings.getInstance().getToolPath();
        if (!configured.isBlank()) {
            ToolCommand tool = fromPath(configured.trim());
            if (tool != null) return tool;
        }

        for (String variable : new String[]{"FACTORIO_MOD_REPO", "FACTORIO_MOD_TOOL"}) {
            String value = System.getenv(variable);
            if (value != null && !value.isBlank()) {
                ToolCommand tool = fromPath(value.trim());
                if (tool != null) return tool;
            }
        }

        Path repository = findRepository(project);
        if (repository != null) {
            ToolCommand tool = fromRepository(repository);
            if (tool != null) return tool;
        }

        if (onPath("factorio-mod")) {
            return new ToolCommand(List.of("factorio-mod"), null, "factorio-mod (from PATH)");
        }

        return null;
    }

    /**
     * Turns whatever the user typed into a command: a repository root, a script, a dll, or a
     * program name.
     *
     * @param value The configured value.
     * @return The command, or null when the value points at nothing.
     */
    public static @Nullable ToolCommand fromPath(String value) {
        Path path = Path.of(value);
        String name = value.toLowerCase(Locale.ROOT);

        if (Files.isDirectory(path)) {
            return fromRepository(path);
        }

        if (Files.isRegularFile(path)) {
            if (name.endsWith(".dll")) {
                return new ToolCommand(List.of("dotnet", path.toString()), parentRepository(path), path.toString());
            }
            return new ToolCommand(List.of(path.toString()), parentRepository(path), path.toString());
        }

        // Not a path at all: a program on PATH, e.g. "factorio-mod".
        return onPath(value) ? new ToolCommand(List.of(value), null, value + " (from PATH)") : null;
    }

    /**
     * Builds the command for a repository root: the .cmd on Windows, the built dll anywhere else.
     *
     * @param repository The repository root.
     * @return The command, or null when the repository has no runnable tool in it.
     */
    public static @Nullable ToolCommand fromRepository(Path repository) {
        Path cmd = repository.resolve("factorio-mod.cmd");
        if (isWindows() && Files.isRegularFile(cmd)) {
            return new ToolCommand(List.of(cmd.toString()), repository, cmd.toString());
        }

        Path dll = repository.resolve("tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll");
        if (Files.isRegularFile(dll)) {
            return new ToolCommand(List.of("dotnet", dll.toString()), repository, "dotnet " + dll);
        }

        // A repository that has never been built: the .cmd builds the tool first, so prefer it even
        // off Windows when it is all there is (bash can run it through the shell it declares only on
        // Windows, so this is the last resort and the message says what to do).
        if (Files.isRegularFile(cmd)) {
            return new ToolCommand(List.of(cmd.toString()), repository,
                    cmd + " (build the tool with 'make tool' if this fails)");
        }

        return null;
    }

    /**
     * Walks up from the open project - and from the working directory - looking for a checkout.
     *
     * @param project The open project, or null.
     * @return The repository root, or null.
     */
    public static @Nullable Path findRepository(@Nullable Project project) {
        if (project != null && project.getBasePath() != null) {
            Path found = walkUp(Path.of(project.getBasePath()));
            if (found != null) return found;
        }
        return walkUp(Path.of("").toAbsolutePath());
    }

    /** The first directory at or above {@code start} that is a checkout. */
    private static @Nullable Path walkUp(Path start) {
        Path directory = start;
        while (directory != null) {
            if (Files.isRegularFile(directory.resolve(REPOSITORY_MARKER))) return directory;
            directory = directory.getParent();
        }
        return null;
    }

    /** The repository a dll inside {@code tools/} belongs to, so builds run from there. */
    private static @Nullable Path parentRepository(Path file) {
        for (Path directory = file.getParent(); directory != null; directory = directory.getParent()) {
            if (Files.isRegularFile(directory.resolve(REPOSITORY_MARKER))) return directory;
        }
        return null;
    }

    /** Whether a program name resolves to an executable on PATH. */
    private static boolean onPath(String program) {
        String path = System.getenv("PATH");
        if (path == null) return false;

        for (String entry : path.split(java.io.File.pathSeparator)) {
            if (entry.isBlank()) continue;
            Path candidate = Path.of(entry).resolve(program);
            if (Files.isExecutable(candidate)) return true;
            if (isWindows()) {
                for (String extension : new String[]{".cmd", ".bat", ".exe"}) {
                    if (Files.isExecutable(Path.of(entry).resolve(program + extension))) return true;
                }
            }
        }
        return false;
    }

    /** Whether this is Windows, where the tool is reached through a batch file. */
    public static boolean isWindows() {
        return System.getProperty("os.name", "").toLowerCase(Locale.ROOT).contains("win");
    }

    /** Runs the tool once to prove it works, returning what it printed. */
    static String describe(@Nullable Project project) throws IOException {
        ToolCommand tool = locate(project);
        return tool == null ? "not found" : tool.description();
    }
}
