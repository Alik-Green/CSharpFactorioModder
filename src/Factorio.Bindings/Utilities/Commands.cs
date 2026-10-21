using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// Console commands, registered with the boilerplate already done.
    /// <para>
    /// The native call wants a localised help text and a handler that has to dig the player out of
    /// the event table and decide what to do when the command came from the console. This wrapper
    /// hands the handler a <see cref="ChatCommand"/> instead: the player (or <c>null</c>), the
    /// argument with the whitespace trimmed off, and a <see cref="ChatCommand.Reply"/> that answers
    /// whoever typed it.
    /// </para>
    /// <code>
    /// Commands.Add("gas", "gas-extractor.command-gas-help", command =&gt;
    /// {
    ///     if (command.Player == null) { command.Reply(Loc.Key("gas-extractor.message-no-player")); return; }
    ///     Extractors.Retarget(command.Parameter);
    /// });
    /// </code>
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Commands
    {
        /// <summary>
        /// Registers a command. The name is what players type after the slash, and must be unique
        /// across all mods; the help text is a locale key, so the wording lives in the locale file
        /// and can be translated.
        /// </summary>
        /// <param name="name">Command name without the slash, e.g. <c>gas</c>.</param>
        /// <param name="helpKey">Locale key of the help text, e.g. <c>gas-extractor.command-gas-help</c>.</param>
        /// <param name="handler">Called whenever the command runs.</param>
        /// @CSharpLua.Template = "util_command({0}, {1}, {2})"
        public static void Add(string name, string helpKey, Action<ChatCommand> handler) => throw new NotImplementedException();
    }

    /// <summary>
    /// What a command handler is given: who typed it and what they typed.
    /// <para>
    /// A compile-time view of the game's command event table - no object is created, so the handler
    /// can read the fields it needs and ignore the rest.
    /// </para>
    /// </summary>
    /// @CSharpLua.Ignore
    public sealed class ChatCommand
    {
        /// <summary>
        /// The player who ran the command, or <c>null</c> when it came from the console (an RCON
        /// connection, a server console, a map editor). Commands that need a player have to check.
        /// </summary>
        /// @CSharpLua.Get = "util_command_player({this})"
        public Player Player => throw new NotImplementedException();

        /// <summary>
        /// The argument the player typed after the command name, trimmed of surrounding
        /// whitespace; <c>null</c> when nothing was typed. It is deliberately unparsed: splitting it
        /// into words or numbers is the mod's decision.
        /// </summary>
        /// @CSharpLua.Get = "util_command_parameter({this})"
        public string Parameter => throw new NotImplementedException();

        /// <summary>
        /// Answers whoever ran the command: <c>player.print</c> when it was a player, <c>game.print</c>
        /// when it came from the console. Accepts a string or a localised string from
        /// <see cref="Loc"/>.
        /// </summary>
        /// <param name="message">The message to show.</param>
        /// @CSharpLua.Template = "util_command_reply({this}, {0})"
        public void Reply(object message) => throw new NotImplementedException();
    }
}
