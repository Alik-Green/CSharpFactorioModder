package com.factorio.csharp.rider;

import com.intellij.ide.impl.ProjectUtil;
import com.intellij.notification.NotificationType;
import com.intellij.openapi.actionSystem.ActionUpdateThread;
import com.intellij.openapi.actionSystem.AnAction;
import com.intellij.openapi.actionSystem.AnActionEvent;
import com.intellij.openapi.progress.ProgressIndicator;
import com.intellij.openapi.progress.ProgressManager;
import com.intellij.openapi.progress.Task;
import com.intellij.openapi.project.Project;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;

/**
 * Tools → New Factorio Mod…: scaffolds a mod and, if asked, builds it once.
 *
 * <p>The scaffolding itself is the tool's job ({@code factorio-mod new}), not the plugin's: the
 * template lives in the repository, so a project created here and one created on the command line
 * are the same project, and there is one place to change what a new mod contains.</p>
 */
public final class NewModAction extends AnAction {

    @Override
    public @NotNull ActionUpdateThread getActionUpdateThread() {
        return ActionUpdateThread.BGT;
    }

    @Override
    public void actionPerformed(@NotNull AnActionEvent event) {
        Project project = event.getProject();

        ToolCommand tool = FactorioToolLocator.locate(project);
        if (tool == null) {
            Notifications.toolMissing(project);
            return;
        }

        NewModDialog dialog = new NewModDialog(project, tool, defaultBaseDirectory(project));
        if (!dialog.showAndGet()) return;

        NewModDialog.Request request = dialog.request();
        create(project, tool, request);
    }

    /**
     * Where a new mod goes by default: the folder open in the IDE when that folder holds one or more
     * mods, otherwise its parent, so a mod made from inside {@code mods/DemoChemistry} lands next to
     * it rather than inside it.
     */
    private static @NotNull Path defaultBaseDirectory(@Nullable Project project) {
        String configured = FactorioSettings.getInstance().getNewModDirectory();
        if (!configured.isBlank()) return Path.of(configured);

        String basePath = project == null ? null : project.getBasePath();
        if (basePath == null) return Path.of(System.getProperty("user.home", "."));

        Path base = Path.of(basePath);
        return Files.isRegularFile(base.resolve("mod.json")) && base.getParent() != null ? base.getParent() : base;
    }

    /** Runs the scaffold, then optionally opens it. */
    private static void create(Project project, ToolCommand tool, NewModDialog.Request request) {
        ProgressManager.getInstance().run(new Task.Backgroundable(project, "Creating " + request.name(), true) {
            private ToolRunner.Result result;
            private String failure;

            @Override
            public void run(@NotNull ProgressIndicator indicator) {
                List<String> arguments = new ArrayList<>();
                arguments.add("new");
                arguments.add(request.name());
                arguments.add("--dir");
                arguments.add(request.directory().toString());
                arguments.add("--title");
                arguments.add(request.title());
                arguments.add("--version");
                arguments.add(request.version());
                arguments.add("--factorio");
                arguments.add(request.factorioVersion());
                if (!request.author().isBlank()) {
                    arguments.add("--author");
                    arguments.add(request.author());
                }
                if (!request.build()) arguments.add("--no-build");

                indicator.setText("Creating " + request.name());
                try {
                    result = ToolRunner.runBlocking(tool, arguments, tool.repository(), indicator);
                } catch (IOException e) {
                    failure = e.getMessage();
                }
            }

            @Override
            public void onSuccess() {
                if (failure != null) {
                    Notifications.show(project, "Could not create " + request.name(),
                            failure + "\n\nTried: " + tool.description(), NotificationType.ERROR);
                    return;
                }

                int exit = result == null ? -1 : result.exitCode();
                if (exit != 0) {
                    Notifications.show(project, "Could not create " + request.name(),
                            tail(result == null ? "" : result.output()), NotificationType.ERROR);
                    return;
                }

                // The tool prints where the zip went; say so rather than making the user read logs.
                String summary = request.build()
                        ? request.name() + " was created and built."
                        : request.name() + " was created (not built yet).";

                if (request.open()) {
                    ProjectUtil.openOrImport(request.directory(), project, true);
                    Notifications.show(project, "Mod created", summary, NotificationType.INFORMATION);
                } else {
                    Notifications.show(project, "Mod created",
                            summary + "\nOpen it with File → Open…, or build it from the command line.",
                            NotificationType.INFORMATION);
                }
            }
        });
    }

    /** The last few lines of a failed run: the useful part. */
    private static String tail(@NotNull String output) {
        String[] lines = output.split("\n");
        int from = Math.max(0, lines.length - 15);
        return String.join("\n", List.of(lines).subList(from, lines.length));
    }
}
