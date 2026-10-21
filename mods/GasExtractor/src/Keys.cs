namespace GasExtractor
{
    /// <summary>
    /// Every localisation key the mod uses, in one place.
    /// <para>
    /// The text itself lives in <c>resources/locale/en/locale.cfg</c>; keeping the keys here means a
    /// typo is a missing constant at compile time instead of a raw <c>{gas-extractor.message-...}</c>
    /// string appearing in a player's chat box.
    /// </para>
    /// </summary>
    public static class Keys
    {
        /// <summary>Help text of <c>/gas</c>.</summary>
        public const string CommandGasHelp = "gas-extractor.command-gas-help";

        /// <summary>Help text of <c>/gas-status</c>.</summary>
        public const string CommandStatusHelp = "gas-extractor.command-status-help";

        /// <summary>Shown to the builder of a new machine.</summary>
        public const string Built = "gas-extractor.message-built";

        /// <summary>Confirmation of <c>/gas &lt;name&gt;</c>; arguments: gas, machine count.</summary>
        public const string GasSet = "gas-extractor.message-gas-set";

        /// <summary>Rejected gas name; argument: what the player typed.</summary>
        public const string GasUnknown = "gas-extractor.message-gas-unknown";

        /// <summary>Status report; arguments: machine count, fluid produced.</summary>
        public const string Status = "gas-extractor.message-status";

        /// <summary>Status report when no machines are registered.</summary>
        public const string StatusNone = "gas-extractor.message-status-none";

        /// <summary>Answer to a command that needs a player but came from the console.</summary>
        public const string NoPlayer = "gas-extractor.message-no-player";
    }
}
