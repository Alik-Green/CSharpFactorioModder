using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// The events almost every mod needs, subscribed once and spelled the way the mod thinks about
    /// them: something was built, something went away, one second passed.
    /// <para>
    /// The raw API splits these across several events that a mod has to remember - an entity can be
    /// built by a player, by a construction robot, by a script, or on a space platform, and can be
    /// removed by hand, by robots, by a script, or by being destroyed - and forgetting one is how
    /// mods end up with registries that point at machines that no longer exist. Each helper below
    /// subscribes to every variant, so a handler is complete by construction.
    /// </para>
    /// <code>
    /// public static void Init()
    /// {
    ///     Lifecycle.OnBuilt(Extractors.MachineName, (machine, builder) =&gt; Extractors.Register(machine, builder));
    ///     Lifecycle.OnRemoved(Extractors.MachineName, machine =&gt; Extractors.Forget(machine));
    ///     Lifecycle.EverySecond(Extractors.Supervise);
    /// }
    /// </code>
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Lifecycle
    {
        /// <summary>
        /// Runs once, when the save is created or the mod is added to an existing one. Storage is
        /// writable here, which is the place to fill in data an old save does not have.
        /// </summary>
        /// <param name="handler">The initialisation handler.</param>
        /// @CSharpLua.Template = "util_on_init({0})"
        public static void OnInit(Action handler) => throw new NotImplementedException();

        /// <summary>
        /// Every way an entity can be built: placed by a player, placed by a construction robot,
        /// raised by a script, or built on a space platform.
        /// <para>
        /// The handler receives the entity and the player responsible, which is <c>null</c> when no
        /// player built it (robots, scripts). Watch one prototype by name, or pass <c>null</c> to
        /// hear every build.
        /// </para>
        /// </summary>
        /// <param name="prototype">Prototype name to watch, e.g. <c>gas-extractor</c>; <c>null</c> for all.</param>
        /// <param name="handler">Called with the built entity and its builder, if any.</param>
        /// @CSharpLua.Template = "util_on_built({0}, {1})"
        public static void OnBuilt(string prototype, Action<Entity, Player> handler) => throw new NotImplementedException();

        /// <summary>
        /// Every way an entity can go away: mined by a player, mined by robots, mined on a space
        /// platform, destroyed, killed, or removed by a script.
        /// <para>
        /// The entity is still readable inside the handler, so anything that has to be read before
        /// it disappears (its contents, its recipe) can be. Registering to this instead of to one
        /// mining event is what keeps a registry free of dead entries.
        /// </para>
        /// </summary>
        /// <param name="prototype">Prototype name to watch, e.g. <c>gas-extractor</c>; <c>null</c> for all.</param>
        /// <param name="handler">Called with the entity that is going away.</param>
        /// @CSharpLua.Template = "util_on_removed({0}, {1})"
        public static void OnRemoved(string prototype, Action<Entity> handler) => throw new NotImplementedException();

        /// <summary>
        /// Runs a handler every 60 ticks, i.e. once a second. Cheaper and steadier than
        /// <c>on_tick</c> for anything that only samples the world, such as counting production.
        /// </summary>
        /// <param name="handler">The per-second handler.</param>
        /// @CSharpLua.Template = "util_every_second({0})"
        public static void EverySecond(Action handler) => throw new NotImplementedException();
    }
}
