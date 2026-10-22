using System;
using System.Collections.Generic;

namespace Factorio.Data
{
    /// <summary>Module effect types supported by a machine's <see cref="AssemblingMachineDefinition.AllowedEffects"/>.</summary>
    [Flags]
    public enum EffectType
    {
        /// <summary>No module effects set (use inherited defaults).</summary>
        None = 0,
        /// <summary>Speed modules / beacons.</summary>
        Speed = 1 << 0,
        /// <summary>Productivity modules.</summary>
        Productivity = 1 << 1,
        /// <summary>Efficiency (power consumption) modules.</summary>
        Consumption = 1 << 2,
        /// <summary>Pollution-modifying modules.</summary>
        Pollution = 1 << 3,
        /// <summary>Quality modules (Factorio 2.0+).</summary>
        Quality = 1 << 4,
        /// <summary>All standard module effects.</summary>
        All = Speed | Productivity | Consumption | Pollution | Quality,
    }

    /// <summary>
    /// A machine that turns recipes into items or fluids - the entity behind an assembling machine,
    /// a chemical plant, an oil refinery, a centrifuge, or an atmospheric condenser.
    /// <code>
    /// public sealed class AtmosphericCondenser : AssemblingMachineDefinition
    /// {
    ///     public AtmosphericCondenser() : base("atmospheric-condenser")
    ///     {
    ///         Icon = Asset.Mod("graphics/entity/atmospheric-condenser.png");
    ///         CollisionBox = BoundingBox.Collision(3, 3);
    ///         SelectionBox = BoundingBox.Selection(3, 3);
    ///         Graphics = GraphicsSet.Auto(Asset.Mod("graphics/entity/atmospheric-condenser.png"), tiles: 3);
    ///         CraftingCategories = [CraftingCategory.Chemistry];
    ///         CraftingSpeed = 1.0;
    ///         EnergyUsage = Energy.Kilowatts(180);
    ///         EnergySource = EnergySource.Electric();
    ///         FluidBoxes = [FluidBox.Output(FluidId.Water, 100, [PipePort.East(1, 0)])];
    ///         MinableResult = "atmospheric-condenser";
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
        /// Declares an assembling machine that starts from an existing vanilla machine: every field
        /// this definition does not set is copied from <paramref name="inheritFrom"/>.
        /// </summary>
        /// <param name="name">Entity prototype name.</param>
        /// <param name="inheritFrom">The prototype to build on, e.g. <see cref="Vanilla.ChemicalPlant"/>.</param>
        public AssemblingMachineDefinition(string name, VanillaPrototype inheritFrom)
            : base("assembling-machine", name)
        {
            Field("__inherit", inheritFrom);
        }

        /// <summary>
        /// The recipe categories this machine can craft:
        /// e.g. <c>[CraftingCategory.Chemistry, CraftingCategory.Of("atmospheric")]</c>.
        /// </summary>
        public CategoryList? CraftingCategories
        {
            get => Get("crafting_categories") as CategoryList;
            set => Set("crafting_categories", value);
        }

        /// <summary>How fast it crafts; 1 is the speed recipes are written for.</summary>
        public double CraftingSpeed
        {
            get => Number("crafting_speed");
            set => Set("crafting_speed", value);
        }

        /// <summary>Power draw while crafting, e.g. <c>Energy.Kilowatts(250)</c>.</summary>
        public EnergyValue? EnergyUsage
        {
            get => Get("energy_usage") as string;
            set => Set("energy_usage", value?.Formatted);
        }

        /// <summary>
        /// Optional fixed recipe name — when set, the machine automatically runs this recipe as soon
        /// as placed without requiring the player to open the machine GUI and select a recipe!
        /// </summary>
        public RecipeId? FixedRecipe
        {
            get => Get("fixed_recipe") as string;
            set => Set("fixed_recipe", value?.Name);
        }

        /// <summary>
        /// What the machine looks like on the map:
        /// e.g. <c>GraphicsSet.Auto(Asset.Mod("graphics/entity/condenser.png"), tiles: 3)</c>.
        /// </summary>
        public GraphicsSet? Graphics
        {
            get => Get("graphics_set") as GraphicsSet;
            set => Set("graphics_set", value);
        }

        /// <summary>
        /// Power source feeding the machine — e.g. <see cref="EnergySource.Electric"/>,
        /// <see cref="EnergySource.Burner"/>, <see cref="EnergySource.Void"/>, or <see cref="Electric"/>.
        /// </summary>
        public EnergySource? EnergySource
        {
            get => Get("energy_source") as EnergySource;
            set => Set("energy_source", value);
        }

        /// <summary>The machine's fluid boxes, e.g. <c>[FluidBox.Output(FluidId.Water, 100, [PipePort.East()])]</c>.</summary>
        public FluidBoxList? FluidBoxes
        {
            get => Get("fluid_boxes") as FluidBoxList;
            set => Set("fluid_boxes", value);
        }

        /// <summary>Whether the machine draws the current recipe icon overlay (alt-mode).</summary>
        public bool ShowRecipeIcon
        {
            get => Flag("show_recipe_icon");
            set => Set("show_recipe_icon", value);
        }

        /// <summary>Number of module slots inside the machine.</summary>
        public int ModuleSlots
        {
            get => (int)Number("module_slots");
            set => Set("module_slots", value);
        }

        /// <summary>Module effect types accepted by this machine (e.g. <c>EffectType.All</c>).</summary>
        public EffectType AllowedEffects
        {
            get => Get("allowed_effects") is EffectTypeEffects e ? e.Flags : EffectType.None;
            set => Set("allowed_effects", value == EffectType.None ? null : new EffectTypeEffects(value));
        }

        /// <summary>An electric energy source using the standard <c>secondary-input</c> priority.</summary>
        public static EnergySource Electric() => Data.EnergySource.Electric();

        /// <summary>A burner energy source running on chemical fuel.</summary>
        public static EnergySource Burner(double effectivity = 1.0) => Data.EnergySource.Burner(effectivity);

        private sealed class EffectTypeEffects : ILuaValue
        {
            public EffectType Flags { get; }

            public EffectTypeEffects(EffectType flags) => Flags = flags;

            public void Write(LuaWriter writer)
            {
                var names = new List<string>();
                if ((Flags & EffectType.Speed) != 0) names.Add("speed");
                if ((Flags & EffectType.Productivity) != 0) names.Add("productivity");
                if ((Flags & EffectType.Consumption) != 0) names.Add("consumption");
                if ((Flags & EffectType.Pollution) != 0) names.Add("pollution");
                if ((Flags & EffectType.Quality) != 0) names.Add("quality");

                writer.BeginArray();
                foreach (var n in names) writer.Value(n);
                writer.EndArray();
            }
        }
    }
}
