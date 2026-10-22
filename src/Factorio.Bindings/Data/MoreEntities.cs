namespace Factorio.Data
{
    /// <summary>
    /// A furnace machine (<c>type = "furnace"</c>) that automatically chooses its recipe from its input slot
    /// without requiring the player to manually select a recipe in a GUI.
    /// </summary>
    public class FurnaceDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone furnace prototype.</summary>
        public FurnaceDefinition(string name) : base("furnace", name) { }

        /// <summary>Declares a furnace inheriting from a vanilla furnace (e.g. <see cref="Vanilla.Machines.ElectricFurnace"/>).</summary>
        public FurnaceDefinition(string name, VanillaPrototype inheritFrom) : base("furnace", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>Crafting categories processed automatically (e.g. <c>[CraftingCategory.Smelting]</c>).</summary>
        public CategoryList? CraftingCategories
        {
            get => Get("crafting_categories") as CategoryList;
            set => Set("crafting_categories", value);
        }

        /// <summary>Crafting speed multiplier relative to a hand craft.</summary>
        public double CraftingSpeed
        {
            get => Number("crafting_speed");
            set => Set("crafting_speed", value);
        }

        /// <summary>Number of input item stacks (usually <c>1</c>).</summary>
        public int SourceInventorySize
        {
            get => (int)Number("source_inventory_size");
            set => Set("source_inventory_size", value);
        }

        /// <summary>Number of output item stacks (usually <c>1</c> or <c>2</c>).</summary>
        public int ResultInventorySize
        {
            get => (int)Number("result_inventory_size");
            set => Set("result_inventory_size", value);
        }

        /// <summary>Active power draw, e.g. <c>Energy.Kilowatts(180)</c> or <c>"180kW"</c>.</summary>
        public EnergyValue? EnergyUsage
        {
            get => Get("energy_usage") as string;
            set => Set("energy_usage", value?.Formatted);
        }

        /// <summary>How the furnace is powered, e.g. <c>EnergySource.Electric(emissionsPerMinute: 1)</c> or <c>EnergySource.Burner()</c>.</summary>
        public EnergySource? EnergySource
        {
            get => Get("energy_source") as EnergySource;
            set => Set("energy_source", value);
        }

        /// <summary>Number of module slots.</summary>
        public double ModuleSlots
        {
            get => Number("module_slots");
            set => Set("module_slots", value);
        }

        /// <summary>Optional fluid inputs/outputs on the furnace.</summary>
        public FluidBoxList? FluidBoxes
        {
            get => Get("fluid_boxes") as FluidBoxList;
            set => Set("fluid_boxes", value);
        }

        /// <summary>Graphics set / sprite rendered in the world (same property name as <see cref="AssemblingMachineDefinition.Graphics"/>).</summary>
        public GraphicsSet? Graphics
        {
            get => Get("graphics_set") as GraphicsSet;
            set => Set("graphics_set", value);
        }

        /// <summary>Alias for <see cref="Graphics"/>.</summary>
        public GraphicsSet? GraphicsSet
        {
            get => Graphics;
            set => Graphics = value;
        }
    }

    /// <summary>
    /// A fluid storage tank (<c>type = "storage-tank"</c>), e.g. inheriting from <see cref="Vanilla.Machines.StorageTank"/>.
    /// </summary>
    public class StorageTankDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone storage tank prototype.</summary>
        public StorageTankDefinition(string name) : base("storage-tank", name) { }

        /// <summary>Declares a storage tank inheriting from <see cref="Vanilla.Machines.StorageTank"/>.</summary>
        public StorageTankDefinition(string name, VanillaPrototype inheritFrom) : base("storage-tank", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>The tank's internal fluid box (capacity and pipe ports).</summary>
        public FluidBox? FluidBox
        {
            get => Get("fluid_box") as FluidBox;
            set => Set("fluid_box", value);
        }

        /// <summary>Length of fluid flow animation in ticks (vanilla default <c>360</c>).</summary>
        public int FlowLengthInTicks
        {
            get => (int)Number("flow_length_in_ticks");
            set => Set("flow_length_in_ticks", value);
        }

        /// <summary>Whether the tank rotates through two orientations instead of four.</summary>
        public bool TwoDirectionOnly
        {
            get => Flag("two_direction_only");
            set => Set("two_direction_only", value);
        }
    }

    /// <summary>
    /// An item chest / warehouse container (<c>type = "container"</c>), e.g. inheriting from <see cref="Vanilla.Machines.SteelChest"/>.
    /// </summary>
    public class ContainerDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone container chest prototype.</summary>
        public ContainerDefinition(string name) : base("container", name) { }

        /// <summary>Declares a container inheriting from a vanilla chest (e.g. <see cref="Vanilla.Machines.SteelChest"/>).</summary>
        public ContainerDefinition(string name, VanillaPrototype inheritFrom) : base("container", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>Number of item stacks stored by the container (e.g. <c>48</c> for steel chest, <c>200</c> for warehouse).</summary>
        public int InventorySize
        {
            get => (int)Number("inventory_size");
            set => Set("inventory_size", value);
        }

        /// <summary>World sprite displayed for the container.</summary>
        public Sprite? Picture
        {
            get => Get("picture") as Sprite;
            set => Set("picture", value);
        }
    }

    /// <summary>
    /// A solar panel entity (<c>type = "solar-panel"</c>), e.g. inheriting from <see cref="Vanilla.Machines.SolarPanel"/>.
    /// </summary>
    public class SolarPanelDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone solar panel prototype.</summary>
        public SolarPanelDefinition(string name) : base("solar-panel", name) { }

        /// <summary>Declares a solar panel inheriting from <see cref="Vanilla.Machines.SolarPanel"/>.</summary>
        public SolarPanelDefinition(string name, VanillaPrototype inheritFrom) : base("solar-panel", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>Peak daytime power output, e.g. <c>Energy.Kilowatts(120)</c> (vanilla is <c>60kW</c>).</summary>
        public EnergyValue? Production
        {
            get => Get("production") as string;
            set => Set("production", value?.Formatted);
        }

        /// <summary>Energy network connection (defaults to electric primary output when inherited).</summary>
        public EnergySource? EnergySource
        {
            get => Get("energy_source") as EnergySource;
            set => Set("energy_source", value);
        }

        /// <summary>World sprite displayed for the solar panel.</summary>
        public Sprite? Picture
        {
            get => Get("picture") as Sprite;
            set => Set("picture", value);
        }
    }

    /// <summary>
    /// An electric energy accumulator (<c>type = "accumulator"</c>), e.g. inheriting from <see cref="Vanilla.Machines.Accumulator"/>.
    /// </summary>
    public class AccumulatorDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone accumulator prototype.</summary>
        public AccumulatorDefinition(string name) : base("accumulator", name) { }

        /// <summary>Declares an accumulator inheriting from <see cref="Vanilla.Machines.Accumulator"/>.</summary>
        public AccumulatorDefinition(string name, VanillaPrototype inheritFrom) : base("accumulator", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>Configures capacity and charge/discharge power limits in one call (vanilla: <c>5MJ</c>, <c>300kW</c>, <c>300kW</c>).</summary>
        public void SetBattery(EnergyValue bufferCapacity, EnergyValue inputFlowLimit, EnergyValue outputFlowLimit)
        {
            SetNested("energy_source", "type", "electric");
            SetNested("energy_source", "usage_priority", "tertiary");
            SetNested("energy_source", "buffer_capacity", bufferCapacity.Formatted);
            SetNested("energy_source", "input_flow_limit", inputFlowLimit.Formatted);
            SetNested("energy_source", "output_flow_limit", outputFlowLimit.Formatted);
        }
    }

    /// <summary>
    /// A resource mining drill or pumpjack (<c>type = "mining-drill"</c>), e.g. inheriting from <see cref="Vanilla.Machines.ElectricMiningDrill"/>.
    /// </summary>
    public class MiningDrillDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone mining drill prototype.</summary>
        public MiningDrillDefinition(string name) : base("mining-drill", name) { }

        /// <summary>Declares a mining drill inheriting from <see cref="Vanilla.Machines.ElectricMiningDrill"/> or <see cref="Vanilla.Machines.Pumpjack"/>.</summary>
        public MiningDrillDefinition(string name, VanillaPrototype inheritFrom) : base("mining-drill", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>Mining speed in cycles per second (vanilla electric drill is <c>0.5</c>).</summary>
        public double MiningSpeed
        {
            get => Number("mining_speed");
            set => Set("mining_speed", value);
        }

        /// <summary>Radius from centre within which resources are mined (vanilla electric drill is <c>2.49</c> for 5×5 area).</summary>
        public double ResourceSearchingRadius
        {
            get => Number("resource_searching_radius");
            set => Set("resource_searching_radius", value);
        }

        /// <summary>Power consumption, e.g. <c>Energy.Kilowatts(90)</c>.</summary>
        public EnergyValue? EnergyUsage
        {
            get => Get("energy_usage") as string;
            set => Set("energy_usage", value?.Formatted);
        }

        /// <summary>How the drill is powered.</summary>
        public EnergySource? EnergySource
        {
            get => Get("energy_source") as EnergySource;
            set => Set("energy_source", value);
        }

        /// <summary>Number of module slots.</summary>
        public double ModuleSlots
        {
            get => Number("module_slots");
            set => Set("module_slots", value);
        }
    }

    /// <summary>
    /// A research laboratory (<c>type = "lab"</c>), e.g. inheriting from <see cref="Vanilla.Machines.Lab"/>.
    /// </summary>
    public class LabDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone lab prototype.</summary>
        public LabDefinition(string name) : base("lab", name) { }

        /// <summary>Declares a lab inheriting from <see cref="Vanilla.Machines.Lab"/>.</summary>
        public LabDefinition(string name, VanillaPrototype inheritFrom) : base("lab", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>Research speed multiplier (vanilla lab is <c>1.0</c>).</summary>
        public double ResearchingSpeed
        {
            get => Number("researching_speed");
            set => Set("researching_speed", value);
        }

        /// <summary>Science pack item names accepted by this lab, e.g. <c>[Vanilla.Items.RedScience, Vanilla.Items.GreenScience]</c>.</summary>
        public StringList? Inputs
        {
            get => Get("inputs") as StringList;
            set => Set("inputs", value);
        }

        /// <summary>Power consumption, e.g. <c>Energy.Kilowatts(60)</c>.</summary>
        public EnergyValue? EnergyUsage
        {
            get => Get("energy_usage") as string;
            set => Set("energy_usage", value?.Formatted);
        }

        /// <summary>Number of module slots.</summary>
        public double ModuleSlots
        {
            get => Number("module_slots");
            set => Set("module_slots", value);
        }
    }

    /// <summary>
    /// A module effect beacon (<c>type = "beacon"</c>), e.g. inheriting from <see cref="Vanilla.Machines.Beacon"/>.
    /// </summary>
    public class BeaconDefinition : EntityDefinition
    {
        /// <summary>Declares a standalone beacon prototype.</summary>
        public BeaconDefinition(string name) : base("beacon", name) { }

        /// <summary>Declares a beacon inheriting from <see cref="Vanilla.Machines.Beacon"/>.</summary>
        public BeaconDefinition(string name, VanillaPrototype inheritFrom) : base("beacon", name) =>
            Field("__inherit", inheritFrom);

        /// <summary>Tile distance from the edge of the beacon that receives transmitted module effects (vanilla is <c>3</c>).</summary>
        public double SupplyAreaDistance
        {
            get => Number("supply_area_distance");
            set => Set("supply_area_distance", value);
        }

        /// <summary>Multiplier applied to transmitted module strengths (vanilla is <c>1.5</c> in Factorio 2.0).</summary>
        public double DistributionEffectivity
        {
            get => Number("distribution_effectivity");
            set => Set("distribution_effectivity", value);
        }

        /// <summary>Power draw, e.g. <c>Energy.Kilowatts(480)</c>.</summary>
        public EnergyValue? EnergyUsage
        {
            get => Get("energy_usage") as string;
            set => Set("energy_usage", value?.Formatted);
        }

        /// <summary>Number of module slots.</summary>
        public double ModuleSlots
        {
            get => Number("module_slots");
            set => Set("module_slots", value);
        }
    }
}
