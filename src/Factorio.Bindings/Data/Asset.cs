using System;

namespace Factorio.Data
{
    /// <summary>
    /// Paths to graphics and sounds.
    /// <para>
    /// A prototype's <c>icon</c> (or a sprite's <c>filename</c>) is a path inside a mod's folder.
    /// Reusing the base game's own art is the usual way to get a prototype that looks right without
    /// drawing anything, and it is what <see cref="Base"/> is for.
    /// </para>
    /// <code>
    /// Icon = Asset.Base("graphics/icons/fluid/water.png"),
    /// </code>
    /// </summary>
    public static class Asset
    {
        /// <summary>
        /// A file inside the base game, e.g. <c>graphics/icons/iron-plate.png</c>. The
        /// <c>__base__/</c> prefix is added; a path that already carries a <c>__mod__/</c> prefix is
        /// left as it is, so nothing is prefixed twice.
        /// </summary>
        /// <param name="path">Path relative to the base game's folder.</param>
        /// <returns>The path the game resolves.</returns>
        public static string Base(string path) =>
            path.StartsWith("__", StringComparison.Ordinal) ? path : "__base__/" + path;

        /// <summary>
        /// The mod whose files <see cref="Mod"/> resolves against, as its <c>info.json</c> names it.
        /// <para>
        /// The build sets this to the mod being built before the definitions run. Every path a
        /// prototype carries has to start with <c>__source__/</c>, and the source of a mod's own
        /// file is the mod itself - <c>__DemoChemistry__/graphics/x.png</c> - so this is what makes
        /// <see cref="Mod"/> write the prefix.
        /// </para>
        /// </summary>
        private static readonly System.Threading.AsyncLocal<string?> CurrentSource = new();

        public static string? Source
        {
            get => CurrentSource.Value;
            set => CurrentSource.Value = value;
        }

        /// <summary>
        /// A file inside this mod, e.g. <c>graphics/entity/my-machine.png</c>, as
        /// <c>__&lt;mod name&gt;__/graphics/entity/my-machine.png</c>.
        /// <para>
        /// The prefix is not decoration: Factorio rejects every resource path that does not start
        /// with <c>__source__/</c> ("not matching the resource path pattern"), so a bare
        /// <c>graphics/x.png</c> in a prototype stops the game from loading the mod.
        /// </para>
        /// </summary>
        /// <param name="path">Path relative to the mod's folder.</param>
        /// <returns>The path the game resolves.</returns>
        public static string Mod(string path)
        {
            if (path.StartsWith("__", StringComparison.Ordinal)) return path;

            var relative = path.TrimStart('/', '\\');
            return string.IsNullOrEmpty(Source) ? relative : $"__{Source}__/{relative}";
        }

        /// <summary>
        /// A sprite table the build rendered from a model, e.g.
        /// <c>Asset.Generated("gas-extractor-sprite")</c> for <c>assets/render.json</c>'s output.
        /// <para>
        /// The render step writes its tables as <c>generated/&lt;name&gt;.lua</c>, and the data stage
        /// can <c>require</c> them, so the value written is
        /// <c>require("generated.&lt;name&gt;")</c> - Lua the build cannot evaluate but the game can.
        /// <paramref name="key"/> picks one entry of that table, e.g. <c>"north"</c>.
        /// </para>
        /// </summary>
        /// <param name="name">The generated table's name, as <c>assets/render.json</c> names it.</param>
        /// <param name="key">An entry of that table, or <c>null</c> for the whole table.</param>
        /// <returns>A value that writes itself as a Lua expression.</returns>
        public static ILuaValue Generated(string name, string? key = null) =>
            new GeneratedAsset(key == null
                ? $"require(\"generated.{name}\")"
                : $"require(\"generated.{name}\").{key}");

        /// <summary>Lua source written verbatim, for <see cref="Generated"/>.</summary>
        private sealed class GeneratedAsset : ILuaValue
        {
            private readonly string _lua;

            public GeneratedAsset(string lua) => _lua = lua;

            public void Write(LuaWriter writer) => writer.Raw(_lua);
        }
    }
}
