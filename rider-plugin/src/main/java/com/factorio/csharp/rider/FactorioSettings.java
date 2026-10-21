package com.factorio.csharp.rider;

import com.intellij.openapi.application.ApplicationManager;
import com.intellij.openapi.components.PersistentStateComponent;
import com.intellij.openapi.components.Service;
import com.intellij.openapi.components.State;
import com.intellij.openapi.components.Storage;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

/**
 * What the plugin remembers between runs.
 *
 * <p>Everything here is about <em>where</em> the tool is and who is writing the mod; everything
 * about <em>how</em> a mod is built lives in that mod's {@code mod.json}, so two mods can be built
 * differently and no setting has to be remembered per project.</p>
 */
@Service(Service.Level.APP)
@State(name = "FactorioModSettings", storages = @Storage("factorio-mod.xml"))
public class FactorioSettings implements PersistentStateComponent<FactorioSettings.State> {

    /** The persisted values. Public fields: the platform serialises them by name. */
    public static final class State {
        /**
         * Where factorio-mod is. May be the repository root, a {@code factorio-mod.cmd}, the
         * {@code factorio-mod.dll}, or a command on PATH. Empty means "find it".
         */
        public String toolPath = "";

        /** Author written into a new mod's mod.json. Empty means the system user name. */
        public String author = "";

        /** Directory new mods are created in. Empty means the folder of the open project. */
        public String newModDirectory = "";

        /** Whether a new mod is built once before the dialog closes. */
        public boolean buildAfterCreate = true;

        /** Whether a new mod is opened in a new window. */
        public boolean openAfterCreate = true;
    }

    private State state = new State();

    /** The single instance the platform keeps. */
    public static FactorioSettings getInstance() {
        return ApplicationManager.getApplication().getService(FactorioSettings.class);
    }

    @Override
    public @NotNull State getState() {
        return state;
    }

    @Override
    public void loadState(@NotNull State state) {
        this.state = state;
    }

    /** The configured tool path, or an empty string when nothing is configured. */
    public String getToolPath() {
        return state.toolPath == null ? "" : state.toolPath;
    }

    /** Remembers where the tool is. */
    public void setToolPath(@Nullable String path) {
        state.toolPath = path == null ? "" : path;
    }

    /** The configured author, or an empty string. */
    public String getAuthor() {
        return state.author == null ? "" : state.author;
    }

    /** Remembers who is writing the mod. */
    public void setAuthor(@Nullable String author) {
        state.author = author == null ? "" : author;
    }

    /** The directory new mods go into, or an empty string for "next to the current project". */
    public String getNewModDirectory() {
        return state.newModDirectory == null ? "" : state.newModDirectory;
    }

    /** Remembers the directory new mods go into. */
    public void setNewModDirectory(@Nullable String directory) {
        state.newModDirectory = directory == null ? "" : directory;
    }

    /** Whether a new mod is built once, so the first thing the user sees is a loadable zip. */
    public boolean isBuildAfterCreate() {
        return state.buildAfterCreate;
    }

    /** Remembers the build-after-create choice. */
    public void setBuildAfterCreate(boolean build) {
        state.buildAfterCreate = build;
    }

    /** Whether a new mod is opened in a new window. */
    public boolean isOpenAfterCreate() {
        return state.openAfterCreate;
    }

    /** Remembers the open-after-create choice. */
    public void setOpenAfterCreate(boolean open) {
        state.openAfterCreate = open;
    }
}
