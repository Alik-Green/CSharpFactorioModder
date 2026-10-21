package com.factorio.csharp.rider;

import com.intellij.openapi.fileChooser.FileChooserDescriptor;
import com.intellij.openapi.fileChooser.FileChooserDescriptorFactory;
import com.intellij.openapi.project.Project;
import com.intellij.openapi.ui.DialogWrapper;
import com.intellij.openapi.ui.TextBrowseFolderListener;
import com.intellij.openapi.ui.TextFieldWithBrowseButton;
import com.intellij.openapi.ui.ValidationInfo;
import com.intellij.ui.components.JBCheckBox;
import com.intellij.ui.components.JBLabel;
import com.intellij.ui.components.JBTextField;
import com.intellij.util.ui.FormBuilder;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import javax.swing.JComboBox;
import javax.swing.JComponent;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import java.io.File;
import java.nio.file.Path;

/**
 * What a new mod is called and where it goes.
 *
 * <p>The dialog asks only for what the template cannot work out: the name (which becomes the mod
 * name, the namespace and the file names), where it goes, and the two strings Factorio shows in the
 * mod list. Everything else - compiler, bindings, API version, build settings - is the repository's
 * business.</p>
 */
public final class NewModDialog extends DialogWrapper {

    private final JBTextField nameField = new JBTextField();
    private final TextFieldWithBrowseButton directoryField = new TextFieldWithBrowseButton();
    private final JBTextField titleField = new JBTextField();
    private final JBTextField authorField = new JBTextField();
    private final JBTextField versionField = new JBTextField("0.1.0");
    private final JComboBox<String> factorioVersion = new JComboBox<>(new String[]{"2.0", "2.1"});
    private final JBCheckBox buildNow = new JBCheckBox("Build once after creating", true);
    private final JBCheckBox openNow = new JBCheckBox("Open in a new window", true);

    private final Path baseDirectory;
    private final String toolDescription;

    /** True once the user has edited the directory themselves, after which it stops following the name. */
    private boolean directoryEdited;

    /**
     * @param project        The project the dialog belongs to, or null from the welcome screen.
     * @param tool           The resolved tool, shown at the bottom so it is obvious what will run.
     * @param baseDirectory  Where a new mod is created unless the user says otherwise.
     */
    public NewModDialog(@Nullable Project project, @NotNull ToolCommand tool, @NotNull Path baseDirectory) {
        super(project, true);
        this.baseDirectory = baseDirectory;
        this.toolDescription = tool.description();

        setTitle("New Factorio Mod");
        setOKButtonText("Create");

        String author = FactorioSettings.getInstance().getAuthor();
        authorField.setText(author.isBlank() ? System.getProperty("user.name", "") : author);
        directoryField.setText(baseDirectory.toString());

        FileChooserDescriptor descriptor = FileChooserDescriptorFactory.createSingleFolderDescriptor();
        directoryField.addBrowseFolderListener(new TextBrowseFolderListener(descriptor, project));

        // A new mod is usually <parent>/<name>; keep the two fields in step until the user takes over.
        nameField.getDocument().addDocumentListener(new DocumentListener() {
            @Override public void insertUpdate(DocumentEvent e) { nameChanged(); }
            @Override public void removeUpdate(DocumentEvent e) { nameChanged(); }
            @Override public void changedUpdate(DocumentEvent e) { nameChanged(); }
        });
        directoryField.getTextField().getDocument().addDocumentListener(new DocumentListener() {
            @Override public void insertUpdate(DocumentEvent e) { directoryEdited = true; }
            @Override public void removeUpdate(DocumentEvent e) { directoryEdited = true; }
            @Override public void changedUpdate(DocumentEvent e) { directoryEdited = true; }
        });
        titleField.setToolTipText("Shown in Factorio's mod list. Left empty, it is made from the name.");

        init();
    }

    /** The mod's parent directory: the field is the mod folder itself, so its parent is where it goes. */
    private void nameChanged() {
        if (directoryEdited) return;
        String name = nameField.getText().trim();
        directoryField.setText(baseDirectory.resolve(name.isEmpty() ? "" : name).toString());
    }

    @Override
    protected @Nullable JComponent createCenterPanel() {
        FormBuilder form = FormBuilder.createFormBuilder()
                .addLabeledComponent(new JBLabel("Name:"), nameField)
                .addLabeledComponent(new JBLabel("Directory:"), directoryField)
                .addLabeledComponent(new JBLabel("Title:"), titleField)
                .addLabeledComponent(new JBLabel("Author:"), authorField)
                .addLabeledComponent(new JBLabel("Version:"), versionField)
                .addLabeledComponent(new JBLabel("Factorio:"), factorioVersion)
                .addComponent(buildNow)
                .addComponent(openNow)
                .addComponent(new JBLabel("Tool: " + toolDescription));

        return form.getPanel();
    }

    @Override
    protected @Nullable ValidationInfo doValidate() {
        String name = nameField.getText().trim();
        if (name.isEmpty()) return new ValidationInfo("A mod needs a name.", nameField);

        String problem = validateName(name);
        if (problem != null) return new ValidationInfo(problem, nameField);

        String directory = directoryField.getText().trim();
        if (directory.isEmpty()) return new ValidationInfo("Choose where the mod goes.", directoryField);

        Path path = Path.of(directory);
        if (!path.getFileName().toString().equalsIgnoreCase(name)) {
            // Not fatal - the folder may be named anything - but it is nearly always a mistake, and
            // Factorio shows the folder name nowhere, so a mismatch is only ever confusing.
            return new ValidationInfo(
                    "The folder should be called '" + name + "' (it holds the mod; rename it or change the name).",
                    directoryField);
        }

        File parent = path.getParent() == null ? null : path.getParent().toFile();
        if (parent == null || (!parent.isDirectory() && !parent.mkdirs())) {
            return new ValidationInfo("Cannot create a folder in " + path.getParent() + ".", directoryField);
        }

        String version = versionField.getText().trim();
        if (!version.matches("\\d+\\.\\d+(\\.\\d+)?")) {
            return new ValidationInfo("Write the version as 0.1.0.", versionField);
        }

        return null;
    }

    /** Factorio's rule: letters, digits, underscore and hyphen, starting with a letter. */
    private static @Nullable String validateName(String name) {
        if (!Character.isLetter(name.charAt(0))) return "The name must start with a letter.";
        for (char c : name.toCharArray()) {
            if (!Character.isLetterOrDigit(c) && c != '_' && c != '-') {
                return "'" + c + "' is not allowed: letters, digits, _ and - only.";
            }
        }
        return null;
    }

    /** What the user asked for. */
    public record Request(
            @NotNull String name,
            @NotNull Path directory,
            @NotNull String title,
            @NotNull String author,
            @NotNull String version,
            @NotNull String factorioVersion,
            boolean build,
            boolean open) {
    }

    /** The request, once the dialog has been accepted. */
    public @NotNull Request request() {
        String name = nameField.getText().trim();
        String title = titleField.getText().trim();
        return new Request(
                name,
                Path.of(directoryField.getText().trim()).toAbsolutePath().normalize(),
                title.isEmpty() ? name : title,
                authorField.getText().trim(),
                versionField.getText().trim(),
                (String) factorioVersion.getSelectedItem(),
                buildNow.isSelected(),
                openNow.isSelected());
    }
}
