# Factorio C# Mods — Rider plugin

Front end for `factorio-mod`: create a mod from the template, and build it from the IDE.

The built plugin is at [`../plugin/FactorioCSharpMods-1.0.0.zip`](../plugin). Install it with
Settings → Plugins → ⚙ → *Install Plugin from Disk…*.

## Sources

```
src/main/resources/META-INF/plugin.xml   the plugin descriptor: id, actions, settings page
src/main/java/com/factorio/csharp/rider/
  NewModAction.java        Tools → New Factorio Mod…, runs `factorio-mod new`
  NewModDialog.java        name, folder, title, author, version, Factorio version
  BuildModAction.java      Tools/project view → Build Factorio Mod, runs `factorio-mod build`
  ToolRunner.java          runs the tool: waiting (create) and streaming (build)
  FactorioToolLocator.java finds the tool: settings, env vars, the repository, PATH
  ToolCommand.java         the resolved command (a .cmd, or dotnet + the dll)
  FactorioSettings.java    what the plugin remembers (application-level)
  FactorioConfigurable.java Settings → Tools → Factorio C# Mods
  Notifications.java       one notification group, one place messages come from
  OpenSettingsAction.java  Tools → Factorio C# Mods Settings…
```

Nothing here is Rider-specific: the plugin depends only on `com.intellij.modules.platform`, so it
loads in Rider, IntelliJ IDEA and the rest, and it does not break when Rider's own APIs move.

## Building

```bat
factorio-mod.cmd plugin
```

or `make plugin` from the repository root. That command is `tools/Factorio.ModTool/Commands/PluginCommand.cs`,
which does what Gradle would do for a plugin this size:

1. find a JDK 17+ (`--jdk`, `FACTORIO_MOD_JDK`, `JAVA_HOME`, `PATH`, the cache) or download one;
2. find or download the IntelliJ Platform (`--idea`, default 2024.2.5) and take its `lib/*.jar`;
3. `javac --release 17` over `src/main/java`;
4. jar the classes with `src/main/resources` (`META-INF/plugin.xml`, icons);
5. zip that jar as `lib/factorio-mod-rider.jar` under one top-level folder.

Output: `build/plugin/FactorioCSharpMods-<version>.zip`, copied to `plugin/`.

The version lives in `plugin.xml` only.

### Gradle, if you would rather

The layout is the standard one (`src/main/java`, `src/main/resources/META-INF/plugin.xml`), so the
IntelliJ Platform Gradle Plugin picks it up as it is:

```kotlin
plugins { id("org.jetbrains.intellij.platform") version "2.1.0" }
intellijPlatform { intellijIdeaCommunity("2024.2.5") }
```

`javac` was chosen for the repository's build because it needs no Gradle distribution and no
network access once the platform is cached.

## Compatibility

| plugin | compiled against | verified |
| --- | --- | --- |
| 1.0.0 | IntelliJ Platform 2024.2.5 (`since-build=242`) | IntelliJ Plugin Verifier: *Compatible* against IC-242.24807.4 and IC-262.10968.63 |

Verified with JetBrains' own `intellij-plugin-verifier` (`check-plugin`), which resolves every
class, method and extension in `plugin.xml` against the IDE: the plugin loads, and the APIs it uses
still exist in the newest platform checked.
