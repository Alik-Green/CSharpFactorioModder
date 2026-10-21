using System;

namespace Factorio.Data
{
    /// <summary>
    /// A machine that turns recipes into items or fluids - the entity behind an assembling machine,
    /// a chemical plant, an oil refinery or a centrifuge.
    /// <para>
    /// The recipe categories it can run are what makes it a chemical plant rather than an
    /// assembling machine: a recipe is only craftable in a machine that lists one of the recipe's
    /// categories. Building on a vanilla prototype (see <see cref="Vanilla"/>) inherits that
    /// prototype's graphics, collision box and energy source, which is the quickest way to a working
    /// machine - give it an icon, a category, a speed and fluid boxes, and it runs.
    /// </para>
    /// <code>
    /// public sealed class ChemicalAssembler : AssemblingMachineDefinition
    /// {
    ///     public ChemicalAssembler()
    ///         : base("demo-chemical-assembler", Vanilla.AssemblingMachine1)
    ///     {
    ///         Icon = Asset.Base("graphics/icons/assembling-machine-1.png");
    ///         IconSize = 64;
    ///         CraftingCategories = ["chemistry"];
    ///         CraftingSpeed = 1.25;
    ///         EnergyUsage = Energy.Kilowatts(250);
    ///         MinableResult = ItemRef.Of("demo-chemical-assembler");
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class AssemblingMachineDefinition : EntityDefinition
    {
        /// <summary>Declares an assembling machine from scratch.</summary>
        /// <param name="name">Entity prototype name.</param>
        public AssemblingMachineDefinition(string name) : base("assembling-machine", name)
        {
            Flags = EntityFlag.PlaceableNeutral | EntityFlag.PlaceablePlayer | EntityFlag.PlayerCreation;
        }

        /// <summary>
        /// Declares an assembling machine that starts from an existing one: every field this
        /// definition does not set is copied from <paramref name="inheritFrom"/> when the data
        /// stage runs.
        /// </summary>
        /// <param name="name">Entity prototype name.</param>
        /// <param name="inheritFrom">The prototype to build on, e.g. <see cref="Vanilla.AssemblingMachine1"/>.</param>
        public AssemblingMachineDefinition(string name, VanillaPrototype inheritFrom)
            : base("assembling-machine", name)
        {
            Field("__inherit", inheritFrom);
        }

        /// <summary>
        /// The recipe categories this machine can craft:
        /// <c>[CraftingCategory.Chemistry, CraftingCategory.Of("my-category")]</c>.
        /// <para>
        /// A recipe is only craftable in a machine that lists one of the recipe's categories, so a
        /// custom category is how a machine is kept to its own recipes. The build checks that every
        /// category names a <c>recipe-category</c> that exists.
        /// </para>
        /// </summary>
        public Values? CraftingCategories
        {
            get => Get("crafting_categories") as Values;
            set => Set("crafting_categories", value);
        }

        /// <summary>How fast it crafts; 1 is the speed recipes are written for.</summary>
        public double CraftingSpeed
        {
            get => Number("crafting_speed");
            set => Set("crafting_speed", value);
        }

        /// <summary>Power draw while crafting, e.g. <c>Energy.Kilowatts(250)</c>.</summary>
        public string? EnergyUsage
        {
            get => Get("energy_usage") as string;
            set => Set("energy_usage", value);
        }

        /// <summary>
        /// What the machine looks like: <c>GraphicsSet.Of("sprite.png", 192, 192, 0.5)</c>.
        /// <para>
        /// Leave it unset when building on a vanilla machine - the vanilla graphics are inherited -
        /// and set it for a machine built from scratch, which the game refuses to load without
        /// graphics of some kind.
        /// </para>
        /// </summary>
        public ILuaValue? Graphics
        {
            get => Get("graphics_set") as ILuaValue;
            set => Set("graphics_set", value);
        }

        /// <summary>
        /// The energy source. Leave it unset when building on a vanilla machine (it is inherited);
        /// use <see cref="Electric"/> or <see cref="Burner"/> for a machine built from scratch.
        /// </summary>
        public object? EnergySource
        {
            get => Get("energy_source");
            set => Set("energy_source", value);
        }

        /// <summary>The machine's fluid boxes, e.g. <c>[FluidBox.Input(...)]</c>.</summary>
        public Values? FluidBoxes
        {
            get => Get("fluid_boxes") as Values;
            set => Set("fluid_boxes", value);
        }



        /// <summary>Whether an open machine window shows the recipe's ingredients.</summary>
        public bool ShowRecipeIcon
        {
            get => Flag("show_recipe_icon");
            set => Set("show_recipe_icon", value);
        }

        /// <summary>An electric energy source using the usual secondary-input priority.</summary>
        /// <returns>The <c>energy_source</c> table.</returns>
        public static ILuaValue Electric() => new EnergySourceTable(new[]
        {
            new System.Collections.Generic.KeyValuePair<string, object?>("type", "electric"),
            new System.Collections.Generic.KeyValuePair<string, object?>("usage_priority", "secondary-input"),
        });

        /// <summary>A burner energy source, for a machine that runs on chemical fuel.</summary>
        /// <param name="effectivity">How much of the fuel's energy it uses; 1 is normal.</param>
        /// <returns>The <c>energy_source</c> table.</returns>
        public static ILuaValue Burner(double effectivity) => new EnergySourceTable(new[]
        {
            new System.Collections.Generic.KeyValuePair<string, object?>("type", "burner"),
            new System.Collections.Generic.KeyValuePair<string, object?>("effectivity", effectivity),
            new System.Collections.Generic.KeyValuePair<string, object?>("fuel_categories", Values.Of("chemical")),
        });

        /// <summary>An energy source table, so <see cref="Electric"/> and <see cref="Burner"/> stay one-liners.</summary>
        private sealed class EnergySourceTable : ILuaValue
        {
            private readonly System.Collections.Generic.KeyValuePair<string, object?>[] _fields;

            public EnergySourceTable(System.Collections.Generic.KeyValuePair<string, object?>[] fields) => _fields = fields;

            public void Write(LuaWriter writer)
            {
                writer.BeginTable();
                foreach (var field in _fields)
                {
                    writer.Key(field.Key);
                    writer.Value(field.Value);
                }
                writer.EndTable();
            }
        }
    }
}
