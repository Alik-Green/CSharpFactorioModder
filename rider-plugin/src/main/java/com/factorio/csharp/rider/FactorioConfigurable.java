package com.factorio.csharp.rider;

import com.intellij.openapi.fileChooser.FileChooserDescriptor;
import com.intellij.openapi.fileChooser.FileChooserDescriptorFactory;
import com.intellij.openapi.options.Configurable;
import com.intellij.openapi.options.ConfigurationException;
import com.intellij.openapi.ui.TextBrowseFolderListener;
import com.intellij.openapi.ui.TextFieldWithBrowseButton;
import com.intellij.ui.components.JBCheckBox;
import com.intellij.ui.components.JBLabel;
import com.intellij.ui.components.JBTextField;
import com.intellij.util.ui.FormBuilder;
import org.jetbrains.annotations.Nullable;

import javax.swing.JComponent;
import java.nio.file.Path;

/**
 * Settings → Tools → Factorio C# Mods.
 *
 * <p>Only the things that belong to the person, not to a mod: where the tool is, who is writing, and
 * where new mods go. How a mod is built lives in that mod's {@code mod.json}, which is right, because
 * it is the mod that has to be reproducible.</p>
 */
public final class FactorioConfigurable implements Configurable {

    private final JBTextField toolPath = new JBTextField();
    private final JBTextField author = new JBTextField();
    private final TextFieldWithBrowseButton newModDirectory = new TextFieldWithBrowseButton();
    private final JBCheckBox buildAfterCreate = new JBCheckBox("Build a new mod once after creating it");
    private final JBCheckBox openAfterCreate = new JBCheckBox("Open a new mod in a new window");
    private final JBLabel resolved = new JBLabel();

    @Override
    public String getDisplayName() {
        return "Factorio C# Mods";
    }

    @Override
    public @Nullable JComponent createComponent() {
        toolPath.setToolTipText("Repository root, factorio-mod.cmd, factorio-mod.dll, or a command on PATH. "
                + "Empty: found from the open project, FACTORIO_MOD_REPO, or PATH.");
        toolPath.getDocument().addDocumentListener(new SimpleDocumentListener(this::updateResolved));

        FileChooserDescriptor descriptor = FileChooserDescriptorFactory.createSingleFolderDescriptor();
        newModDirectory.addBrowseFolderListener(new TextBrowseFolderListener(descriptor));

        return FormBuilder.createFormBuilder()
                .addLabeledComponent(new JBLabel("factorio-mod:"), toolPath)
                .addComponent(new JBLabel("Empty means: find it in the open project or on PATH."))
                .addComponent(resolved)
                .addLabeledComponent(new JBLabel("Author:"), author)
                .addLabeledComponent(new JBLabel("New mods in:"), newModDirectory)
                .addComponent(buildAfterCreate)
                .addComponent(openAfterCreate)
                .getPanel();
    }

    /** Shows what the current tool path resolves to, so a typo is visible before a build fails. */
    private void updateResolved() {
        ToolCommand tool = toolPath.getText().isBlank()
                ? FactorioToolLocator.locate(null)
                : FactorioToolLocator.fromPath(toolPath.getText().trim());

        resolved.setText(tool == null
                ? "Not found - set the path above."
                : "Resolves to: " + tool.description());
    }

    @Override
    public boolean isModified() {
        FactorioSettings settings = FactorioSettings.getInstance();
        return !toolPath.getText().trim().equals(settings.getToolPath())
                || !author.getText().trim().equals(settings.getAuthor())
                || !newModDirectory.getText().trim().equals(settings.getNewModDirectory())
                || buildAfterCreate.isSelected() != settings.isBuildAfterCreate()
                || openAfterCreate.isSelected() != settings.isOpenAfterCreate();
    }

    @Override
    public void apply() throws ConfigurationException {
        String path = toolPath.getText().trim();
        if (!path.isEmpty() && FactorioToolLocator.fromPath(path) == null) {
            throw new ConfigurationException(
                    "'" + path + "' is not a factorio-mod: give the repository root, factorio-mod.cmd, "
                            + "factorio-mod.dll, or a command on PATH.");
        }

        FactorioSettings settings = FactorioSettings.getInstance();
        settings.setToolPath(path);
        settings.setAuthor(author.getText().trim());
        settings.setNewModDirectory(newModDirectory.getText().trim());
        settings.setBuildAfterCreate(buildAfterCreate.isSelected());
        settings.setOpenAfterCreate(openAfterCreate.isSelected());
    }

    @Override
    public void reset() {
        FactorioSettings settings = FactorioSettings.getInstance();
        toolPath.setText(settings.getToolPath());
        author.setText(settings.getAuthor());
        newModDirectory.setText(settings.getNewModDirectory().isBlank()
                ? ""
                : Path.of(settings.getNewModDirectory()).toString());
        buildAfterCreate.setSelected(settings.isBuildAfterCreate());
        openAfterCreate.setSelected(settings.isOpenAfterCreate());
        updateResolved();
    }

    @Override
    public void disposeUIResources() {
        // The components go with the panel; nothing is registered elsewhere.
    }

    /** One-method DocumentListener, for the field that triggers a re-resolve. */
    private static final class SimpleDocumentListener implements javax.swing.event.DocumentListener {
        private final Runnable onChange;

        SimpleDocumentListener(Runnable onChange) {
            this.onChange = onChange;
        }

        @Override public void insertUpdate(javax.swing.event.DocumentEvent e) { onChange.run(); }
        @Override public void removeUpdate(javax.swing.event.DocumentEvent e) { onChange.run(); }
        @Override public void changedUpdate(javax.swing.event.DocumentEvent e) { onChange.run(); }
    }
}
