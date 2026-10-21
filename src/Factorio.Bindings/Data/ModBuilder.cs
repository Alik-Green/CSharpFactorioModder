using System;
using System.Collections.Generic;

namespace Factorio.Data
{
    /// <summary>
    /// Collects the prototypes of a mod, in the order <see cref="ModDefinition.Define"/> adds them.
    /// <para>
    /// One is created by the generated entry point and handed to the mod's
    /// <see cref="ModDefinition"/>. A definition is a C# object; what the builder keeps is a plain
    /// copy of its fields, which is what ends up in <c>data.lua</c>.
    /// </para>
    /// </summary>
    public sealed class ModBuilder
    {
        private readonly List<PrototypeDefinition> _prototypes = new();

        /// <summary>
        /// Adds one definition. Order matters only for readability: Factorio resolves
        /// cross-references after every prototype is registered.
        /// </summary>
        /// <param name="prototype">The definition to add.</param>
        public void Add(PrototypeDefinition prototype)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));
            _prototypes.Add(prototype);
        }

        /// <summary>Everything added so far, in order. Read by the toolchain when it writes data.lua.</summary>
        public IReadOnlyList<PrototypeDefinition> Prototypes => _prototypes;
    }
}
