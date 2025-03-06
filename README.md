# BetterNPCEditor

This is a basic editor I created to help me edit JSON files used by the Rust plugin Better NPC.

To use it, load your directory with your oxide/data/BetterNpc files, it will load subfolders as well.
Select a .json file on the left side and it will load the configuration on the right side.
Double click to edit entries. 

To export sections you can select Export after highlighting a specific branch you want to export like a single NPC setting or all NPC settings, Belt items, Wear Items, etc... The export button will enable when you have selected a compatible object.
When importing multiple items you may need to delete the existing objects first, importing a single item will replace the item selected.

When you are done editing, press save changes and upload the config file to your server.
Reload your BetterNpc plugin and your server should be using the new config.

You will need the .net 8 desktop runtime to make this work. Either download the release or compile yourself using Visul Studio.
[.net 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
