using System;
using System.Collections.Generic;

namespace Factorio.Data
{
    /// <summary>
    /// Collects the prototypes (and optional mod settings) of a mod, in the order
    /// <see cref="ModDefinition.Define"/> adds them.
    /// </summary>
    public sealed class ModBuilder
    {
        private readonly List<PrototypeDefinition> _prototypes = new();
        private readonly HashSet<string> _seen = new(StringComparer.Ordinal);

        /// <summary>
        /// Adds one definition (plus its companion <see cref="ItemDefinition"/> and
        /// <see cref="RecipeDefinition"/> automatically if <see cref="EntityDefinition.Craftable"/> was called).
        /// </summary>
        /// <param name="prototype">The definition to add.</param>
        public void Add(PrototypeDefinition prototype)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));
            AddSingle(prototype);
            if (prototype is EntityDefinition entity)
            {
                foreach (var companion in entity.CreateCompanionDefinitions())
                    AddIfAbsent(companion);
            }
        }

        /// <summary>
        /// Adds multiple prototype definitions in one call:
        /// <c>mod.Add(new CondenseWaterRecipe(), new AtmosphericCondenser(), new CondenserTech());</c>
        /// </summary>
        public void Add(params PrototypeDefinition[] prototypes)
        {
            if (prototypes == null) throw new ArgumentNullException(nameof(prototypes));
            foreach (var p in prototypes)
                Add(p);
        }

        private void AddSingle(PrototypeDefinition prototype)
        {
            var key = $"{prototype.Type}/{prototype.Name}";
            // If an auto-generated companion was added earlier and the author also added an
            // explicit definition with the same type/name, let the explicit definition win cleanly.
            if (!_seen.Add(key))
            {
                var idx = _prototypes.FindIndex(p => p.Type == prototype.Type && p.Name == prototype.Name);
                if (idx >= 0) _prototypes[idx] = prototype;
                return;
            }
            _prototypes.Add(prototype);
        }

        private void AddIfAbsent(PrototypeDefinition companion)
        {
            var key = $"{companion.Type}/{companion.Name}";
            if (_seen.Add(key))
                _prototypes.Add(companion);
        }

        /// <summary>Everything added so far, in order. Read by the toolchain when it writes data.lua / settings.lua.</summary>
        public IReadOnlyList<PrototypeDefinition> Prototypes => _prototypes;
    }
}
