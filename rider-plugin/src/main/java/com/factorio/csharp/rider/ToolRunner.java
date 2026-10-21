package com.factorio.csharp.rider;

import com.intellij.execution.configurations.GeneralCommandLine;
import com.intellij.execution.filters.RegexpFilter;
import com.intellij.execution.filters.TextConsoleBuilderFactory;
import com.intellij.execution.process.OSProcessHandler;
import com.intellij.execution.process.ProcessListener;
import com.intellij.execution.process.ProcessEvent;
import com.intellij.execution.ui.ConsoleView;
import com.intellij.execution.ui.ConsoleViewContentType;
import com.intellij.execution.ui.RunContentDescriptor;
import com.intellij.execution.ui.RunContentManager;
import com.intellij.execution.executors.DefaultRunExecutor;
import com.intellij.openapi.progress.ProgressIndicator;
import com.intellij.openapi.project.Project;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.nio.file.Path;
import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;

/**
 * Runs the tool, in the two ways an IDE needs it.
 *
 * <ul>
 *   <li>{@link #runBlocking}: for a short command whose result decides what happens next - creating
 *       a mod - where the output is needed before the dialog goes away.</li>
 *   <li>{@link #runInConsole}: for a build, which is long, streams, and whose output the user wants
 *       to read, search and click. The console gets a filter that turns
 *       {@code file.cs:12: message} into a link, which is the format the tool prints its findings
 *       in.</li>
 * </ul>
 */
public final class ToolRunner {

    private ToolRunner() {
    }

    /** What a finished run produced. */
    public record Result(int exitCode, @NotNull String output) {
        /** Whether the command succeeded. */
        public boolean ok() {
            return exitCode == 0;
        }
    }

    /**
     * Runs the tool and waits, collecting its output.
     *
     * <p>stdout and stderr are merged: the tool writes findings to stderr and progress to stdout,
     * and they only make sense interleaved in the order they were produced.</p>
     *
     * @param tool        The tool command.
     * @param arguments   Its arguments.
     * @param workDir     Where to run, or null for the command's default.
     * @param indicator   Where to show the current line, or null.
     * @return Exit code and everything printed.
     * @throws IOException When the process cannot be started.
     */
    public static @NotNull Result runBlocking(
            @NotNull ToolCommand tool,
            @NotNull List<String> arguments,
            @Nullable Path workDir,
            @Nullable ProgressIndicator indicator) throws IOException {

        GeneralCommandLine line = tool.commandLine(arguments);
        if (workDir != null) line.setWorkDirectory(workDir.toFile());

        ProcessBuilder builder = new ProcessBuilder(line.getCommandLineList(null));
        if (line.getWorkDirectory() != null) builder.directory(line.getWorkDirectory());
        builder.redirectErrorStream(true);

        Process process = builder.start();
        StringBuilder output = new StringBuilder();

        try (BufferedReader reader = new BufferedReader(
                new InputStreamReader(process.getInputStream(), StandardCharsets.UTF_8))) {
            String text;
            while ((text = reader.readLine()) != null) {
                output.append(text).append('\n');
                if (indicator != null) {
                    indicator.setText2(text.strip());
                    if (indicator.isCanceled()) {
                        process.destroy();
                        break;
                    }
                }
            }
        }

        try {
            return new Result(process.waitFor(), output.toString());
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            process.destroy();
            return new Result(-1, output + "\n(interrupted)");
        }
    }

    /**
     * Runs the tool in the Run tool window, streaming.
     *
     * @param project   The open project, whose run content is used.
     * @param tool      The tool command.
     * @param arguments Its arguments.
     * @param title     What the tab is called, e.g. "Build DemoChemistry".
     * @param onFinish  Called with the exit code once it ends, or null.
     */
    public static void runInConsole(
            @NotNull Project project,
            @NotNull ToolCommand tool,
            @NotNull List<String> arguments,
            @NotNull String title,
            @Nullable java.util.function.IntConsumer onFinish) {

        ConsoleView console = TextConsoleBuilderFactory.getInstance()
                .createBuilder(project)
                .getConsole();

        // `  data.lua:0: item x: ...` and `  src/Main.cs:12: ...` both become clickable. The tool
        // prints findings exactly this way, which is why it is worth a filter at all.
        console.addMessageFilter(new RegexpFilter(project, "$FILE_PATH$:$LINE$: $MESSAGE$"));

        OSProcessHandler handler;
        try {
            handler = new OSProcessHandler(tool.commandLine(arguments));
        } catch (com.intellij.execution.ExecutionException e) {
            console.print("Cannot start " + tool.description() + ": " + e.getMessage() + "\n",
                    ConsoleViewContentType.ERROR_OUTPUT);
            show(project, console, handlerFreeDescriptor(console, title));
            return;
        }

        console.attachToProcess(handler);

        AtomicBoolean reported = new AtomicBoolean(false);
        handler.addProcessListener(new ProcessListener() {
            @Override
            public void processTerminated(@NotNull ProcessEvent event) {
                if (!reported.compareAndSet(false, true)) return;
                int exit = event.getExitCode();
                console.print("\n" + (exit == 0 ? "Done." : "Finished with exit code " + exit + ".") + "\n",
                        exit == 0 ? ConsoleViewContentType.SYSTEM_OUTPUT : ConsoleViewContentType.ERROR_OUTPUT);
                if (onFinish != null) onFinish.accept(exit);
            }
        });

        show(project, console, new RunContentDescriptor(console, handler, console.getComponent(), title));
        handler.startNotify();
    }

    /** Shows the console, whether or not there is a process behind it. */
    private static void show(Project project, ConsoleView console, RunContentDescriptor descriptor) {
        RunContentManager.getInstance(project).showRunContent(DefaultRunExecutor.getRunExecutorInstance(), descriptor);
    }

    /** A descriptor for a console with nothing running, so the message is still visible. */
    private static RunContentDescriptor handlerFreeDescriptor(ConsoleView console, String title) {
        return new RunContentDescriptor(console, null, console.getComponent(), title);
    }
}
