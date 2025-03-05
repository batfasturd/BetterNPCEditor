using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better_NCP_Editor
{
    class ToolTips
    {
        public Dictionary<string, string> tips { get; private set; } = new Dictionary<string, string>();


        public ToolTips()
        {
            // General tips
            tips.Add("Enabled? [true/false]", "Enables or disables NPC appearance on the map.");
            tips.Add("Remove other NPCs? [true/false]", "Removes standard NPCs within the monument area.");
            tips.Add("Radius", "NPC appearance radius.");
            tips.Add("Use minimum and maximum values? [true/false]", "Whether to enforce minimum/maximum limits.");

            // Standard Monument tips
            tips.Add("The size of the monument", "Specifies the monument's length and width for random NPC placement and standard NPC removal boundaries.");

            // Custom Monument tips
            tips.Add("Position", "Custom monument position on the map.");
            tips.Add("Rotation", "Custom monument rotation (needed for multi-map NPC placements).");

            // NPC Preset tips
            tips.Add("Minimum numbers - Day", "Minimum NPCs for the day preset.");
            tips.Add("Maximum numbers - Day", "Maximum NPCs for the day preset.");
            tips.Add("Minimum numbers - Night", "Minimum NPCs for the night preset.");
            tips.Add("Maximum numbers - Night", "Maximum NPCs for the night preset.");
            tips.Add("Type of appearance (0 - random; 1 - own list) (not used for Road and Biome)", "Specifies NPC appearance type: 0 for random, 1 for custom list. Not used for roads.");
            tips.Add("Own list of locations (not used for Road and Biome)", "Custom list of NPC spawn locations. Ensure the list meets the maximum NPC count. Not used for roads.");
            tips.Add("Which loot table should the plugin use? (0 - default; 1 - own; 2 - AlphaLoot; 3 - CustomLoot; 4 - loot table of the Rust objects; 5 - combine the 1 and 4 methods)", "Select the NPC loot table type. Type 5 combines types 1 and 4.");
            tips.Add("Loot table from prefabs (if the loot table type is 4 or 5)", "Settings for the Rust loot table. See documentation for details.");
            tips.Add("Own loot table (if the loot table type is 1 or 5)", "Custom NPC loot table. See documentation for details.");

            // NPC Settings tips
            tips.Add("Names", "List of NPC names (chosen randomly).");
            tips.Add("Health", "NPC hit points.");
            tips.Add("Roam Range", "Patrol distance from the spawn point.");
            tips.Add("Chase Range", "Chase distance from the spawn point.");
            tips.Add("Attack Range Multiplier", "Multiplier for the NPC's weapon range.");
            tips.Add("Sense Range", "Target detection radius.");
            tips.Add("Target Memory Duration [sec.]", "Time (in seconds) the NPC remembers a target.");
            tips.Add("Scale damage", "Damage multiplier applied by the NPC.");
            tips.Add("Aim Cone Scale", "Shooting spread (default in Rust is 2; non-negative only).");
            tips.Add("Detect the target only in the NPC's viewing vision cone? [true/false]", "If true, detection is limited to the NPC’s vision cone; false enables 360° detection.");
            tips.Add("Vision Cone", "NPC vision cone angle (20–180°). Not used if detection is 360°.");
            tips.Add("Speed", "NPC movement speed (default in Rust is 5).");
            tips.Add("Minimum time of appearance after death (not used for Events) [sec.]", "Minimum delay for NPC reappearance after death (not used for events).");
            tips.Add("Maximum time of appearance after death (not used for Events) [sec.]", "Maximum delay for NPC reappearance after death (not used for events).");
            tips.Add("Disable radio effects? [true/false]", "Toggle radio effects.");
            tips.Add("Is this a stationary NPC? [true/false]", "If true, the NPC remains stationary.");
            tips.Add("Remove a corpse after death? (it is recommended to use the true value to improve performance) [true/false]", "If true, NPC corpses are removed (recommended for performance).");
            tips.Add("Wear items", "List of NPC clothing and armor.");
            tips.Add("Belt items", "List of quick-access items (e.g., weapons, medkits, grenades).");
            tips.Add("Kits (it is recommended to use the previous 2 settings to improve performance)", "List of NPC kits (leave blank if not used).");

            // Rust loot table tips
            tips.Add("Minimum numbers of prefabs", "Minimum number of prefabs in the loot table.");
            tips.Add("Maximum numbers of prefabs", "Maximum number of prefabs in the loot table.");

            tips.Add("List of prefabs", "List of Rust object prefabs with full paths and drop chances.");

            // Own loot table tips
            tips.Add("Minimum numbers of items", "Minimum number of items in the loot table.");
            tips.Add("Maximum numbers of items", "Maximum number of items in the loot table.");
            tips.Add("List of items", "List of NPC items, including blueprints and custom items.");
        }




    }
}
