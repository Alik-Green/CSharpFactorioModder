package com.factorio.csharp.rider;

import com.intellij.openapi.project.Project;
import com.intellij.openapi.options.ShowSettingsUtil;
import com.intellij.notification.Notification;
import com.intellij.notification.NotificationAction;
import com.intellij.notification.NotificationGroupManager;
import com.intellij.notification.NotificationType;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

/**
 * The one place a notification is raised, so the group id is written once.
 */
public final class Notifications {

    private Notifications() {
    }

    /** The group declared in plugin.xml. */
    public static final String GROUP = "FactorioMod";

    /** Shows a message in the IDE's notification area. */
    public static @NotNull Notification show(
            @NotNull Project project, @NotNull String title, @NotNull String content, @NotNull NotificationType type) {
        Notification notification = NotificationGroupManager.getInstance()
                .getNotificationGroup(GROUP)
                .createNotification(title, content, type);
        notification.notify(project);
        return notification;
    }

    /** Shows a message with one "Open settings" action, for when the tool cannot be found. */
    public static void toolMissing(@NotNull Project project) {
        Notification notification = NotificationGroupManager.getInstance()
                .getNotificationGroup(GROUP)
                .createNotification(
                        "factorio-mod was not found",
                        "Point the plugin at the factorio-csharp repository, a factorio-mod.cmd, or the "
                                + "factorio-mod.dll - or set FACTORIO_MOD_REPO.",
                        NotificationType.ERROR);

        notification.addAction(NotificationAction.createSimpleExpiring(
                "Open settings",
                () -> ShowSettingsUtil.getInstance().showSettingsDialog(project, FactorioConfigurable.class)));

        notification.notify(project);
    }
}
