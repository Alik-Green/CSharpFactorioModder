package com.factorio.csharp.rider;

import com.intellij.notification.NotificationType;
import com.intellij.openapi.actionSystem.ActionUpdateThread;
import com.intellij.openapi.actionSystem.AnAction;
import com.intellij.openapi.actionSystem.AnActionEvent;
import com.intellij.openapi.actionSystem.CommonDataKeys;
import com.intellij.openapi.project.Project;
import com.intellij.openapi.vfs.VirtualFile;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Set;

/**
 * Tools → Build Factorio Mod: runs {@code factorio-mod build} for the mods that were selected.
 *
 * <p>This is the build with the checks and with the clickable output. Rider's own Build button does
 * the same thing through the generated {@code .csproj}; this action exists for the cases the .NET
 * project cannot cover - a mod without one, several mods at once, and reading the checker's
 * findings in a console that links them to the source.</p>
 */
public final class BuildModAction extends AnAction {

    @Override
    public @NotNull ActionUpdateThread getActionUpdateThread() {
        return ActionUpdateThread.BGT;
    }

    @Override
    public void update(@NotNull AnActionEvent event) {
        event.getPresentation().setEnabledAndVisible(!mods(event).isEmpty());
    }

    @Override
    public void actionPerformed(@NotNull AnActionEvent event) {
        Project project = event.getProject();
        if (project == null) return;

        ToolCommand tool = FactorioToolLocator.locate(project);
        if (tool == null) {
            Notifications.toolMissing(project);
            return;
        }

        List<Path> mods = mods(event);
        if (mods.isEmpty()) {
            Notifications.show(project, "No mod to build",
                    "Open a folder with a mod.json in it, or select one in the project view.",
                    NotificationType.WARNING);
            return;
        }

        for (Path mod : mods) {
            ToolRunner.runInConsole(
                    project,
                    tool,
                    List.of("build", mod.toString()),
                    "Build " + mod.getFileName(),
                    exit -> {
                        if (exit != 0) {
                            Notifications.show(project, "Build failed",
                                    mod.getFileName() + " reported problems - see the Build tab.",
                                    NotificationType.ERROR);
                        }
                    });
        }
    }

    /**
     * The mods a build should cover: the selection if it holds any, otherwise the open project, and
     * as a last resort every mod in {@code mods/} of the repository the project belongs to.
     */
    private static @NotNull List<Path> mods(@NotNull AnActionEvent event) {
        Set<Path> found = new LinkedHashSet<>();

        VirtualFile[] selection = event.getData(CommonDataKeys.VIRTUAL_FILE_ARRAY);
        if (selection != null) {
            for (VirtualFile file : selection) {
                Path mod = modDirectoryOf(Path.of(file.getPath()));
                if (mod != null) found.add(mod);
            }
        }

        if (found.isEmpty()) {
            Project project = event.getProject();
            if (project != null && project.getBasePath() != null) {
                Path mod = modDirectoryOf(Path.of(project.getBasePath()));
                if (mod != null) found.add(mod);
            }
        }

        if (found.isEmpty()) {
            Project project = event.getProject();
            if (project != null) {
                Path repository = FactorioToolLocator.findRepository(project);
                if (repository != null) found.addAll(modsIn(repository.resolve("mods")));
            }
        }

        return new ArrayList<>(found);
    }

    /** The mod a file or folder belongs to: itself or its parent, if it holds a mod.json. */
    private static @Nullable Path modDirectoryOf(Path path) {
        if (Files.isRegularFile(path.resolve("mod.json"))) return path;
        Path parent = path.getParent();
        if (parent != null && Files.isRegularFile(parent.resolve("mod.json"))) return parent;
        return null;
    }

    /** Every folder under {@code root} that is a mod. */
    private static List<Path> modsIn(Path root) {
        List<Path> mods = new ArrayList<>();
        if (!Files.isDirectory(root)) return mods;

        try (var children = Files.list(root)) {
            children.filter(Files::isDirectory)
                    .filter(directory -> Files.isRegularFile(directory.resolve("mod.json")))
                    .sorted()
                    .forEach(mods::add);
        } catch (java.io.IOException ignored) {
            // An unreadable directory simply contributes nothing to the build.
        }
        return mods;
    }
}
