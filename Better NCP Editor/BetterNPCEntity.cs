using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Better_NCP_Editor
{
    public class BetterNPCEntity
    {
        // In BetterNPCEntity class
        public BetterNPCEntity()
        {
            Enabled = false;
            MonumentSize = "";
            RemoveOtherNPCs = false;
            Presets = new List<Preset>();
        }

        [JsonPropertyName("Enabled? [true/false]")]
        public bool Enabled { get; set; }

        // Marked as nullable since some JSON files may not include this key.
        [JsonPropertyName("The size of the monument")]
        public string? MonumentSize { get; set; }

        // Marked as nullable so a missing key won’t cause issues.
        [JsonPropertyName("Remove other NPCs? [true/false]")]
        public bool? RemoveOtherNPCs { get; set; }

        [JsonPropertyName("Presets")]
        public List<Preset> Presets { get; set; }
    }

    public class Preset
    {
        // In Preset class
        public Preset()
        {
            Enabled = true;
            MinimumNumbersDay = 1;
            MaximumNumbersDay = 1;
            MinimumNumbersNight = 1;
            MaximumNumbersNight = 1;
            NPCSetting = new NPCSetting();
            Economics = new EconomicsInfo();
            AppearanceType = 0;
            OwnLocations = new List<string>();
            ReturnToAppearance = false;
            NavigationGridType = 0;
            CratePath = "";
            LootTableType = 0;
            PrefabLootTable = new PrefabLootTable();
            OwnLootTable = new OwnLootTable();
        }

        [JsonPropertyName("Enabled? [true/false]")]
        public bool Enabled { get; set; }

        [JsonPropertyName("Minimum numbers - Day")]
        public int MinimumNumbersDay { get; set; }

        [JsonPropertyName("Maximum numbers - Day")]
        public int MaximumNumbersDay { get; set; }

        [JsonPropertyName("Minimum numbers - Night")]
        public int MinimumNumbersNight { get; set; }

        [JsonPropertyName("Maximum numbers - Night")]
        public int MaximumNumbersNight { get; set; }

        [JsonPropertyName("NPCs setting")]
        public NPCSetting NPCSetting { get; set; }

        [JsonPropertyName("The amount of economics that is given for killing the NPC")]
        public EconomicsInfo Economics { get; set; }

        [JsonPropertyName("Type of appearance (0 - random; 1 - own list) (not used for Road and Biome)")]
        public int AppearanceType { get; set; }

        [JsonPropertyName("Own list of locations (not used for Road and Biome)")]
        public List<string> OwnLocations { get; set; }

        [JsonPropertyName("If the NPC ends up below ocean sea level, should the NPC return to it's place of appearance? [true/false]")]
        public bool ReturnToAppearance { get; set; }

        [JsonPropertyName("Type of navigation grid (0 - used mainly on the island, 1 - used mainly under water or under land, as well as outside the map, can be used on some monuments)")]
        public int NavigationGridType { get; set; }

        [JsonPropertyName("The path to the crate that appears at the place of death (empty - not used)")]
        public string CratePath { get; set; }

        [JsonPropertyName("Which loot table should the plugin use? (0 - default; 1 - own; 2 - AlphaLoot; 3 - CustomLoot; 4 - loot table of the Rust objects; 5 - combine the 1 and 4 methods)")]
        public int LootTableType { get; set; }

        [JsonPropertyName("Loot table from prefabs (if the loot table type is 4 or 5)")]
        public PrefabLootTable PrefabLootTable { get; set; }

        [JsonPropertyName("Own loot table (if the loot table type is 1 or 5)")]
        public OwnLootTable OwnLootTable { get; set; }
    }

    public class NPCSetting
    {
        // In NPCSetting class
        public NPCSetting()
        {
            Names = new List<string>();
            Health = 100.0;
            RoamRange = 0.0;
            ChaseRange = 0.0;
            AttackRangeMultiplier = 1.0;
            SenseRange = 0.0;
            TargetMemoryDuration = 0.0;
            ScaleDamage = 1.0;
            AimConeScale = 1.0;
            DetectInVisionCone = false;
            VisionCone = 0.0;
            Speed = 0.0;
            MinAppearanceTime = 0.0;
            MaxAppearanceTime = 0.0;
            DisableRadioEffects = false;
            IsStationary = false;
            RemoveCorpse = false;
            WearItems = new List<WearItem>();
            BeltItems = new List<BeltItem>();
            Kits = new List<object>();
        }

        [JsonPropertyName("Names")]
        public List<string> Names { get; set; }

        [JsonPropertyName("Health")]
        public double Health { get; set; }

        [JsonPropertyName("Roam Range")]
        public double RoamRange { get; set; }

        [JsonPropertyName("Chase Range")]
        public double ChaseRange { get; set; }

        [JsonPropertyName("Attack Range Multiplier")]
        public double AttackRangeMultiplier { get; set; }

        [JsonPropertyName("Sense Range")]
        public double SenseRange { get; set; }

        [JsonPropertyName("Target Memory Duration [sec.]")]
        public double TargetMemoryDuration { get; set; }

        [JsonPropertyName("Scale damage")]
        public double ScaleDamage { get; set; }

        [JsonPropertyName("Aim Cone Scale")]
        public double AimConeScale { get; set; }

        [JsonPropertyName("Detect the target only in the NPC's viewing vision cone? [true/false]")]
        public bool DetectInVisionCone { get; set; }

        [JsonPropertyName("Vision Cone")]
        public double VisionCone { get; set; }

        [JsonPropertyName("Speed")]
        public double Speed { get; set; }

        [JsonPropertyName("Minimum time of appearance after death (not used for Events) [sec.]")]
        public double MinAppearanceTime { get; set; }

        [JsonPropertyName("Maximum time of appearance after death (not used for Events) [sec.]")]
        public double MaxAppearanceTime { get; set; }

        [JsonPropertyName("Disable radio effects? [true/false]")]
        public bool DisableRadioEffects { get; set; }

        [JsonPropertyName("Is this a stationary NPC? [true/false]")]
        public bool IsStationary { get; set; }

        [JsonPropertyName("Remove a corpse after death? (it is recommended to use the true value to improve performance) [true/false]")]
        public bool RemoveCorpse { get; set; }

        [JsonPropertyName("Wear items")]
        public List<WearItem> WearItems { get; set; }

        [JsonPropertyName("Belt items")]
        public List<BeltItem> BeltItems { get; set; }

        [JsonPropertyName("Kits (it is recommended to use the previous 2 settings to improve performance)")]
        public List<object> Kits { get; set; }
    }

    public class WearItem
    {
        // In WearItem class
        public WearItem()
        {
            ShortName = "";
            SkinID = 0;
        }

        [JsonPropertyName("ShortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("SkinID (0 - default)")]
        public long SkinID { get; set; } // Changed from int to long
    }

    public class BeltItem
    {
        // In BeltItem class
        public BeltItem()
        {
            ShortName = "";
            Amount = 0;
            SkinID = 0;
            Mods = new List<string>();
            Ammo = "";
        }


        [JsonPropertyName("ShortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("Amount")]
        public int Amount { get; set; }

        [JsonPropertyName("SkinID (0 - default)")]
        public long SkinID { get; set; } // Changed from int to long

        [JsonPropertyName("Mods")]
        public List<string> Mods { get; set; }

        [JsonPropertyName("Ammo")]
        public string Ammo { get; set; }
    }

    public class LootItem
    {
        // In LootItem class
        public LootItem()
        {
            ShortName = "";
            Minimum = 0;
            Maximum = 0;
            Chance = 0.0;
            IsBlueprint = false;
            SkinID = 0;
            Name = "";
        }

        [JsonPropertyName("ShortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("Minimum")]
        public int Minimum { get; set; }

        [JsonPropertyName("Maximum")]
        public int Maximum { get; set; }

        [JsonPropertyName("Chance [0.0-100.0]")]
        public double Chance { get; set; }

        [JsonPropertyName("Is this a blueprint? [true/false]")]
        public bool IsBlueprint { get; set; }

        [JsonPropertyName("SkinID (0 - default)")]
        public long SkinID { get; set; } // Changed from int to long

        [JsonPropertyName("Name (empty - default)")]
        public string Name { get; set; }
    }

    public class EconomicsInfo
    {
        // In EconomicsInfo class
        public EconomicsInfo()
        {
            Economics = 0.0;
            ServerRewards = 0;
            IQEconomic = 0;
        }

        [JsonPropertyName("Economics")]
        public double Economics { get; set; }

        [JsonPropertyName("Server Rewards (minimum 1)")]
        public int ServerRewards { get; set; }

        [JsonPropertyName("IQEconomic (minimum 1)")]
        public int IQEconomic { get; set; }
    }

    public class PrefabLootTable
    {
        // In PrefabLootTable class
        public PrefabLootTable()
        {
            MinPrefabs = 0;
            MaxPrefabs = 0;
            UseMinMax = false;
            Prefabs = new List<Prefab>();
        }


        [JsonPropertyName("Minimum numbers of prefabs")]
        public int MinPrefabs { get; set; }

        [JsonPropertyName("Maximum numbers of prefabs")]
        public int MaxPrefabs { get; set; }

        [JsonPropertyName("Use minimum and maximum values? [true/false]")]
        public bool UseMinMax { get; set; }

        [JsonPropertyName("List of prefabs")]
        public List<Prefab> Prefabs { get; set; }
    }

    public class Prefab
    {
        // In Prefab class
        public Prefab()
        {
            Chance = 0.0;
            Path = "";
        }

        [JsonPropertyName("Chance [0.0-100.0]")]
        public double Chance { get; set; }

        [JsonPropertyName("The path to the prefab")]
        public string Path { get; set; }
    }

    public class OwnLootTable
    {
        // In OwnLootTable class
        public OwnLootTable()
        {
            MinItems = 0;
            MaxItems = 0;
            UseMinMax = false;
            Items = new List<LootItem>();
        }

        [JsonPropertyName("Minimum numbers of items")]
        public int MinItems { get; set; }

        [JsonPropertyName("Maximum numbers of items")]
        public int MaxItems { get; set; }

        [JsonPropertyName("Use minimum and maximum values? [true/false]")]
        public bool UseMinMax { get; set; }

        [JsonPropertyName("List of items")]
        public List<LootItem> Items { get; set; }
    }

}
