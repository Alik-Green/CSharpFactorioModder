package com.factorio.csharp.rider;

import com.intellij.execution.configurations.GeneralCommandLine;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;

/**
 * The resolved way to start {@code factorio-mod}.
 *
 * <p>The tool is a .NET program, but on Windows it is normally reached through
 * {@code factorio-mod.cmd}, which builds the tool first if it has not been built yet. So what has to
 * be run is not one fixed executable: it is a command prefix - {@code ["C:\repo\factorio-mod.cmd"]}
 * or {@code ["dotnet", "C:\repo\tools\...\factorio-mod.dll"]} - followed by the arguments.</p>
 *
 * @param prefix     The program and any fixed arguments before the tool's own.
 * @param repository The factorio-csharp repository root, when it is known; builds run from there so
 *                   relative paths in the tool's output are stable.
 * @param description What to show the user in a settings dialog, e.g. the command itself.
 */
public record ToolCommand(@NotNull List<String> prefix, @Nullable Path repository, @NotNull String description) {

    /** A command line for one call, with the tool's own arguments appended. */
    public @NotNull GeneralCommandLine commandLine(@NotNull List<String> arguments) {
        List<String> all = new ArrayList<>(prefix);
        all.addAll(arguments);

        GeneralCommandLine line = new GeneralCommandLine(all);
        if (repository != null) {
            line.setWorkDirectory(repository.toFile());
        }
        return line;
    }

    /** The same, for a call written out in the source. */
    public @NotNull GeneralCommandLine commandLine(@NotNull String... arguments) {
        return commandLine(List.of(arguments));
    }
}
