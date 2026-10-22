using System;
using System.Collections.Generic;
using System.Linq;

namespace Factorio.Data
{
    /// <summary>
    /// A tab in the crafting menu: <c>item-subgroup</c> in the prototype API.
    /// <para>
    /// The named members are the base game's subgroups. A mod that wants its own tab declares it
    /// with <see cref="ItemSubgroupDefinition"/> and refers to it by name; the build checks that
    /// every name used is either declared or one of these.
    /// </para>
    /// </summary>
    public sealed class Subgroup : PrototypeName
    {
        private Subgroup(string name) : base(name)
        {
        }

        /// <summary>
        /// A string converts, so <c>Subgroup = "production-machine"</c> works as well as the named
        /// member - which is what makes a custom name (one this mod declares) no harder to write
        /// than a base-game one.
        /// </summary>
        /// <param name="name">The prototype name.</param>
        public static implicit operator Subgroup(string name) => new(name);

        /// <summary>Any subgroup by name, e.g. one this mod declares.</summary>
        /// <param name="name">The <c>item-subgroup</c> name.</param>
        /// <returns>The subgroup.</returns>
        public static Subgroup Of(string name) => new(name);

        /// <summary>Machines that make things: assembling machines, chemical plants, furnaces.</summary>
        public static readonly Subgroup ProductionMachine = new("production-machine");

        /// <summary>Machines that take things out of the ground: drills, pumps, the gas extractor.</summary>
        public static readonly Subgroup ExtractionMachine = new("extraction-machine");

        /// <summary>Furnaces, as opposed to other production machines.</summary>
        public static readonly Subgroup SmeltingMachine = new("smelting-machine");

        /// <summary>Modules and beacons.</summary>
        public static readonly Subgroup Module = new("module");

        /// <summary>Power generation and storage.</summary>
        public static readonly Subgroup Energy = new("energy");

        /// <summary>Things the player holds: blueprints, planners, the map.</summary>
        public static readonly Subgroup Tool = new("tool");

        /// <summary>Space-platform-only machines.</summary>
        public static readonly Subgroup SpaceRelated = new("space-related");

        /// <summary>Chests and other storage.</summary>
        public static readonly Subgroup Storage = new("storage");

        /// <summary>Transport belts.</summary>
        public static readonly Subgroup Belt = new("belt");

        /// <summary>Inserters.</summary>
        public static readonly Subgroup Inserter = new("inserter");

        /// <summary>Pipes, poles and substations.</summary>
        public static readonly Subgroup EnergyPipeDistribution = new("energy-pipe-distribution");

        /// <summary>Trains and signals.</summary>
        public static readonly Subgroup TrainTransport = new("train-transport");

        /// <summary>Cars and tanks.</summary>
        public static readonly Subgroup Transport = new("transport");

        /// <summary>Roboports and logistics chests.</summary>
        public static readonly Subgroup LogisticNetwork = new("logistic-network");

        /// <summary>Combinators and wires.</summary>
        public static readonly Subgroup CircuitNetwork = new("circuit-network");

        /// <summary>Stone, landfill and tiles the player places.</summary>
        public static readonly Subgroup Terrain = new("terrain");

        /// <summary>Recipes that only exist as recipes, such as barrelling.</summary>
        public static readonly Subgroup FluidRecipes = new("fluid-recipes");

        /// <summary>Ores and other raw resources.</summary>
        public static readonly Subgroup RawResource = new("raw-resource");

        /// <summary>Plates, stone and other processed raw materials.</summary>
        public static readonly Subgroup RawMaterial = new("raw-material");

        /// <summary>Empty barrels.</summary>
        public static readonly Subgroup Barrel = new("barrel");

        /// <summary>Barrelling recipes.</summary>
        public static readonly Subgroup FillBarrel = new("fill-barrel");

        /// <summary>Unbarrelling recipes.</summary>
        public static readonly Subgroup EmptyBarrel = new("empty-barrel");

        /// <summary>Plates, gears, circuits: what everything else is made of.</summary>
        public static readonly Subgroup IntermediateProduct = new("intermediate-product");

        /// <summary>Recipes whose product is another recipe's intermediate.</summary>
        public static readonly Subgroup IntermediateRecipe = new("intermediate-recipe");

        /// <summary>Uranium processing.</summary>
        public static readonly Subgroup UraniumProcessing = new("uranium-processing");

        /// <summary>Science packs.</summary>
        public static readonly Subgroup SciencePack = new("science-pack");

        /// <summary>Internal processes the player rarely sees.</summary>
        public static readonly Subgroup InternalProcess = new("internal-process");

        /// <summary>Fluids, as they appear in the crafting menu.</summary>
        public static readonly Subgroup Fluid = new("fluid");

        /// <summary>Guns.</summary>
        public static readonly Subgroup Gun = new("gun");

        /// <summary>Ammunition.</summary>
        public static readonly Subgroup Ammo = new("ammo");

        /// <summary>Capsules.</summary>
        public static readonly Subgroup Capsule = new("capsule");

        /// <summary>Armour.</summary>
        public static readonly Subgroup Armor = new("armor");

        /// <summary>Modular equipment.</summary>
        public static readonly Subgroup Equipment = new("equipment");

        /// <summary>Personal equipment.</summary>
        public static readonly Subgroup UtilityEquipment = new("utility-equipment");

        /// <summary>Equipment that defends the player.</summary>
        public static readonly Subgroup MilitaryEquipment = new("military-equipment");

        /// <summary>Walls and gates.</summary>
        public static readonly Subgroup DefensiveStructure = new("defensive-structure");

        /// <summary>Turrets.</summary>
        public static readonly Subgroup Turret = new("turret");

        /// <summary>Ammo categories, as opposed to ammo items.</summary>
        public static readonly Subgroup AmmoCategory = new("ammo-category");

        /// <summary>The catch-all tab. This is where an item with no subgroup ends up.</summary>
        public static readonly Subgroup Other = new("other");
    }

    /// <summary>
    /// A tab in the crafting menu's left-hand column: <c>item-group</c>.
    /// <para>
    /// The named members are the base game's eleven tabs. A mod that adds its own declares it with
    /// <see cref="ItemGroupDefinition"/> and refers to it by name.
    /// </para>
    /// </summary>
    public sealed class ItemGroup : PrototypeName
    {
        private ItemGroup(string name) : base(name)
        {
        }

        /// <summary>
        /// A string converts, so <c>Subgroup = "production-machine"</c> works as well as the named
        /// member - which is what makes a custom name (one this mod declares) no harder to write
        /// than a base-game one.
        /// </summary>
        /// <param name="name">The prototype name.</param>
        public static implicit operator ItemGroup(string name) => new(name);

        /// <summary>Any item group by name, e.g. one this mod declares.</summary>
        /// <param name="name">The <c>item-group</c> name.</param>
        /// <returns>The group.</returns>
        public static ItemGroup Of(string name) => new(name);

        /// <summary>Belts, inserters, poles, trains and the logistic network.</summary>
        public static readonly ItemGroup Logistics = new("logistics");

        /// <summary>Machines, furnaces, drills, tools and modules.</summary>
        public static readonly ItemGroup Production = new("production");

        /// <summary>Plates, circuits, science packs.</summary>
        public static readonly ItemGroup IntermediateProducts = new("intermediate-products");

        /// <summary>Weapons, ammo, armour and turrets.</summary>
        public static readonly ItemGroup Combat = new("combat");

        /// <summary>Fluids, barrels and barrelling recipes.</summary>
        public static readonly ItemGroup Fluids = new("fluids");

        /// <summary>Virtual signals.</summary>
        public static readonly ItemGroup Signals = new("signals");

        /// <summary>Spawners and other enemy things.</summary>
        public static readonly ItemGroup Enemies = new("enemies");

        /// <summary>Trees, rocks, cliffs, corpses, remnants.</summary>
        public static readonly ItemGroup Environment = new("environment");

        /// <summary>Placeable tiles.</summary>
        public static readonly ItemGroup Tiles = new("tiles");

        /// <summary>Explosions and particles.</summary>
        public static readonly ItemGroup Effects = new("effects");

        /// <summary>Everything that fits nowhere else. This is where an item with no subgroup goes.</summary>
        public static readonly ItemGroup Other = new("other");
    }

    /// <summary>
    /// A recipe category: which machines can craft which recipes.
    /// <para>
    /// The named members are the base game's nine. A mod that invents a category declares it with
    /// <see cref="RecipeCategoryDefinition"/> and refers to it by name - <c>CraftingCategory.Of("gas-extraction")</c>
    /// or just the string, which converts.
    /// </para>
    /// </summary>
    public sealed class CraftingCategory : PrototypeName
    {
        private CraftingCategory(string name) : base(name)
        {
        }

        /// <summary>
        /// A string converts, so <c>Subgroup = "production-machine"</c> works as well as the named
        /// member - which is what makes a custom name (one this mod declares) no harder to write
        /// than a base-game one.
        /// </summary>
        /// <param name="name">The prototype name.</param>
        public static implicit operator CraftingCategory(string name) => new(name);

        /// <summary>Any recipe category by name, e.g. one this mod declares.</summary>
        /// <param name="name">The <c>recipe-category</c> name.</param>
        /// <returns>The category.</returns>
        public static CraftingCategory Of(string name) => new(name);

        /// <summary>Hand crafting in the character's inventory.</summary>
        public static readonly CraftingCategory HandCrafting = new("hand-crafting");

        /// <summary>The player's own crafting, and assembling machines.</summary>
        public static readonly CraftingCategory Crafting = new("crafting");

        /// <summary>Assembling machine 2 and up, and the character.</summary>
        public static readonly CraftingCategory AdvancedCrafting = new("advanced-crafting");

        /// <summary>Furnaces.</summary>
        public static readonly CraftingCategory Smelting = new("smelting");

        /// <summary>Chemical plants, and assembling machines 3.</summary>
        public static readonly CraftingCategory Chemistry = new("chemistry");

        /// <summary>Recipes that consume or produce a fluid in an assembling machine.</summary>
        public static readonly CraftingCategory CraftingWithFluid = new("crafting-with-fluid");

        /// <summary>Oil refineries.</summary>
        public static readonly CraftingCategory OilProcessing = new("oil-processing");

        /// <summary>Rocket silos.</summary>
        public static readonly CraftingCategory RocketBuilding = new("rocket-building");

        /// <summary>Centrifuges.</summary>
        public static readonly CraftingCategory Centrifuging = new("centrifuging");
    }

    /// <summary>
    /// The sort order of a prototype inside its tab, e.g. <c>b[gas-extractor]</c>.
    /// <para>
    /// Factorio sorts by these strings, so their content is up to the mod - the letters only have to
    /// be comparable. <see cref="At"/> writes the usual <c>letter[name]</c> form, which sorts by
    /// letter first and then by name; <see cref="Of"/> and the implicit string conversion take
    /// anything else.
    /// </para>
    /// </summary>
    public sealed class SortOrder : PrototypeName
    {
        private SortOrder(string name) : base(name)
        {
        }

        /// <summary>
        /// A string converts, so <c>Subgroup = "production-machine"</c> works as well as the named
        /// member - which is what makes a custom name (one this mod declares) no harder to write
        /// than a base-game one.
        /// </summary>
        /// <param name="name">The prototype name.</param>
        public static implicit operator SortOrder(string name) => new(name);

        /// <summary>Any order string, used as written.</summary>
        /// <param name="order">The order string.</param>
        /// <returns>The order.</returns>
        public static SortOrder Of(string order) => new(order);

        /// <summary>
        /// The usual form: a sort letter and a name, <c>b[gas-extractor]</c>. Items in one tab are
        /// sorted by the part before the bracket, then by what is inside it.
        /// </summary>
        /// <param name="group">Sort letter or word, e.g. <c>a</c>.</param>
        /// <param name="name">Name shown inside the brackets, usually the prototype's own.</param>
        /// <returns>The order.</returns>
        public static SortOrder At(string group, string name) => new(group + "[" + name + "]");
    }

    /// <summary>
    /// The entity flags: what can be done with an entity. A fixed set the game defines, hence an
    /// enum; a combination is written as the array of names the prototype API expects.
    /// </summary>
    [Flags]
    public enum EntityFlag
    {
        /// <summary>No flags.</summary>
        None = 0,

        /// <summary>Cannot be rotated before or after placing.</summary>
        NotRotatable = 1 << 0,

        /// <summary>Map-editor placement defaults to the neutral force.</summary>
        PlaceableNeutral = 1 << 1,

        /// <summary>Map-editor placement defaults to the player's force.</summary>
        PlaceablePlayer = 1 << 2,

        /// <summary>Map-editor placement defaults to the enemy force.</summary>
        PlaceableEnemy = 1 << 3,

        /// <summary>Does not have to line up with the world grid, like trees and land mines.</summary>
        PlaceableOffGrid = 1 << 4,

        /// <summary>
        /// Built by players: allows blueprinting, deconstructing and repairing, and puts the entity
        /// on the map with the default colour when <c>map_color</c> is unset.
        /// </summary>
        PlayerCreation = 1 << 5,

        /// <summary>Rotates in 45-degree steps rather than 90.</summary>
        BuildingDirection8Way = 1 << 6,

        /// <summary>The game detects the proper direction automatically, as for pumps and train stops.</summary>
        FilterDirections = 1 << 7,

        /// <summary>Can be found with <c>game.get_entity_by_unit_number</c>.</summary>
        GetByUnitNumber = 1 << 8,

        /// <summary>Breathes air, and so is affected by poison.</summary>
        BreathsAir = 1 << 9,

        /// <summary>Cannot be healed with repair packs.</summary>
        NotRepairable = 1 << 10,

        /// <summary>Not drawn on the map.</summary>
        NotOnMap = 1 << 11,

        /// <summary>Cannot be deconstructed.</summary>
        NotDeconstructable = 1 << 12,

        /// <summary>Cannot be put in a blueprint.</summary>
        NotBlueprintable = 1 << 13,

        /// <summary>Hides the alt-mode overlay, such as a machine's recipe icon.</summary>
        HideAltInfo = 1 << 14,

        /// <summary>Does not catch fire.</summary>
        NotFlammable = 1 << 15,

        /// <summary>Inserters and loaders cannot take items out.</summary>
        NoAutomatedItemRemoval = 1 << 16,

        /// <summary>Inserters and loaders cannot put items in.</summary>
        NoAutomatedItemInsertion = 1 << 17,

        /// <summary>Not selected even when it has a selection box.</summary>
        NotSelectableInGame = 1 << 18,

        /// <summary>The upgrade planner skips it.</summary>
        NotUpgradable = 1 << 19,

        /// <summary>Left out of the kill statistics.</summary>
        NotInKillStatistics = 1 << 20,

        /// <summary>Rotates in 22.5-degree steps.</summary>
        BuildingDirection16Way = 1 << 21,

        /// <summary>Snaps to a rail support's spot.</summary>
        SnapToRailSupportSpot = 1 << 22,

        /// <summary>Not listed in a recipe tooltip's "made in".</summary>
        NotInMadeIn = 1 << 23,
    }

    /// <summary>
    /// The item flags: a fixed set the game defines, written as the array of names the prototype
    /// API expects.
    /// </summary>
    [Flags]
    public enum ItemFlag
    {
        /// <summary>No flags.</summary>
        None = 0,

        /// <summary>Draws the logistic areas of roboports while the item is held.</summary>
        DrawLogisticOverlay = 1 << 0,

        /// <summary>The trash-unrequested feature skips this item.</summary>
        ExcludedFromTrashUnrequested = 1 << 1,

        /// <summary>Always shown in filter lists, even when its recipe is locked.</summary>
        AlwaysShow = 1 << 2,

        /// <summary>Not shown in the bonus gui.</summary>
        HideFromBonusGui = 1 << 3,

        /// <summary>Not shown in a burner's fuel tooltip.</summary>
        HideFromFuelTooltip = 1 << 4,

        /// <summary>Cannot be stacked at all.</summary>
        NotStackable = 1 << 5,

        /// <summary>The item is what a machine drops when mined, for items that place several entities.</summary>
        PrimaryPlaceResult = 1 << 6,

        /// <summary>Can be opened from the inventory, like a blueprint book.</summary>
        ModOpenable = 1 << 7,

        /// <summary>Exists only while held, like a deconstruction planner.</summary>
        OnlyInCursor = 1 << 8,

        /// <summary>Can be spawned.</summary>
        Spawnable = 1 << 9,

        /// <summary>Has a spoil result.</summary>
        SpoilResult = 1 << 10,

        /// <summary>Ignores the spoil-time modifier.</summary>
        IgnoreSpoilTimeModifier = 1 << 11,
    }

    /// <summary>
    /// Turns flag enums into the array of names Factorio wants, spelling each member the way the
    /// prototype API does (<c>PlayerCreation</c> is <c>player-creation</c>).
    /// <para>
    /// Named <c>FlagList</c> rather than <c>Flags</c> so it never collides with the <c>Flags</c>
    /// property of a definition, which is what a mod actually assigns.
    /// </para>
    /// </summary>
    public static class FlagList
    {
        /// <summary>Writes an entity flag combination as a Lua array of strings.</summary>
        /// <param name="flags">The combination.</param>
        /// <returns>The value to assign to <c>flags</c>.</returns>
        public static Values Of(EntityFlag flags) => Values.From(
            Enum.GetValues<EntityFlag>()
                .Where(flag => flag != EntityFlag.None && flags.HasFlag(flag))
                .Select(flag => (object?)FactorioName(flag.ToString())));

        /// <summary>Writes an item flag combination as a Lua array of strings.</summary>
        /// <param name="flags">The combination.</param>
        /// <returns>The value to assign to <c>flags</c>.</returns>
        public static Values Of(ItemFlag flags) => Values.From(
            Enum.GetValues<ItemFlag>()
                .Where(flag => flag != ItemFlag.None && flags.HasFlag(flag))
                .Select(flag => (object?)FactorioName(flag.ToString())));

        /// <summary>
        /// <c>PlayerCreation</c> becomes <c>player-creation</c>: the prototype API's spelling is the
        /// C# member name with the capitals opened up, with the few exceptions the game's own names
        /// make for (<c>GetByUnitNumber</c> is <c>get-by-unit-number</c>, not <c>get-by-unit-number</c>
        /// - the rule holds).
        /// </summary>
        /// <param name="member">The enum member's name.</param>
        /// <returns>The prototype API's spelling.</returns>
        private static string FactorioName(string member)
        {
            var builder = new System.Text.StringBuilder(member.Length + 4);
            for (var i = 0; i < member.Length; i++)
            {
                var c = member[i];
                // A capital opens a new word; so does a digit, unless it continues one ("16-way").
                var startsWord = char.IsUpper(c) || (char.IsDigit(c) && i > 0 && !char.IsDigit(member[i - 1]));
                if (startsWord && i > 0) builder.Append('-');
                builder.Append(char.ToLowerInvariant(c));
            }
            return builder.ToString();
        }
    }
}
