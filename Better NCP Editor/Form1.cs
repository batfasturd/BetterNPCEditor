using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Windows.Forms;
using System.Globalization;

namespace Better_NCP_Editor
{
    public partial class Form1 : Form
    {
        private const string formTitle = "Better NCP Editor";
        private const string version = "0.0.2-alpha";
        private string currentJsonFilePath;
        private JsonNode currentJson;
        private bool fileModified = false;
        private ToolTips toolTips = new ToolTips();

        private ToolTip customToolTip = new ToolTip();
        private TreeNode lastHoveredNode = null;

        // Lists for item dropdowns
        private List<String> _wearItems;
        private List<String> _beltItems;
        private List<String> _weaponModItems;

        private Dictionary<String, Dictionary<String, UInt64>> _itemShortnameToSkinName;

        // All items dictionary.
        private Dictionary<String, String> _allItems;

        public Form1()
        {
            InitializeComponent();
            this.Text = $"{formTitle} v{version}";
            this.AutoScaleMode = AutoScaleMode.None;
            dirTreeView.AfterSelect += DirTreeView_AfterSelect;
            entityTreeView.NodeMouseDoubleClick += entityTreeView_NodeMouseDoubleClick;
            entityTreeView.AfterSelect += entityTreeView_AfterSelect;

            searchTextBox.KeyDown += SearchTextBox_KeyDown;

            // Prevent the form from shrinking below its current size.
            this.MinimumSize = new Size(1044, 735);  // Set minimum allowed size
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Example layout using docking:
            dirTreeView.Dock = DockStyle.Left;
            entityTreeView.Dock = DockStyle.Fill;

            // Optionally, set a fixed width for the dirTreeView:
            dirTreeView.Width = 290;

            // In the designer or constructor:
            dirTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            entityTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            statusTextbox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            entityTreeView.ShowNodeToolTips = false;

            // Configure the custom tooltip.
            customToolTip.AutoPopDelay = 15000;  // Show for 15 seconds.
            customToolTip.AutomaticDelay = 1000;
            customToolTip.ShowAlways = true;
            customToolTip.InitialDelay = 500;
            customToolTip.ReshowDelay = 500;
            customToolTip.UseAnimation = true;

            // Subscribe to events.
            entityTreeView.MouseMove += EntityTreeView_MouseMove;
            entityTreeView.MouseLeave += EntityTreeView_MouseLeave;

            // Load the item database.
            _wearItems = LoadItemList("wearitems.json");
            _allItems = LoadItemDatabase("allitems.json");
            _beltItems = LoadItemList("beltitems.json");
            _weaponModItems = LoadItemList("weaponmoditems.json");
            _itemShortnameToSkinName = LoadSkinDict("skins.json");

        }


        private List<String> LoadItemList(String filepath)
        {
            try
            {
                return JsonListLoader.Load(filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading JSON file: " + ex.Message);
                return null;
            }
        }

        private Dictionary<String, String> LoadItemDatabase(String filepath)
        {
            try
            {
                return JsonDictionaryLoader.Load(filepath);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading JSON file: " + ex.Message);
                return null;
            }
        }

        private Dictionary<String, Dictionary<String, UInt64>> LoadSkinDict(String filepath)
        {
            try
            {
                return JsonDictionaryLoader.LoadSkinDict(filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading JSON file: " + ex.Message);
                return null;
            }
        }

        private void EntityTreeView_MouseMove(object sender, MouseEventArgs e)
        {
            int xOffset = 15;
            int yOffset = 0;
            TreeNode node = entityTreeView.GetNodeAt(e.Location);
            if (node != null)
            {
                // Measure the text size for the node using the TreeView's font.
                Size textSize = TextRenderer.MeasureText(node.Text, entityTreeView.Font);
                // Construct a rectangle that represents the area occupied by the text.
                Rectangle textRect = new Rectangle(node.Bounds.X, node.Bounds.Y, textSize.Width, node.Bounds.Height);

                if (textRect.Contains(e.Location))
                {
                    if (node != lastHoveredNode)
                    {
                        lastHoveredNode = node;
                        // Use an offset so the tooltip doesn't overlap the text.
                        Point offsetLocation = new Point(e.Location.X + xOffset, e.Location.Y + xOffset);
                        customToolTip.Show(node.ToolTipText, entityTreeView, offsetLocation, customToolTip.AutoPopDelay);
                    }
                }
                else
                {
                    customToolTip.Hide(entityTreeView);
                    lastHoveredNode = null;
                }
            }
            else
            {
                customToolTip.Hide(entityTreeView);
                lastHoveredNode = null;
            }
        }

        private void EntityTreeView_MouseLeave(object sender, EventArgs e)
        {
            customToolTip.Hide(entityTreeView);
            lastHoveredNode = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Other initialization code, if needed.
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = "Select a directory containing JSON files";
            folderDialog.UseDescriptionForTitle = true;
            folderDialog.ShowNewFolderButton = false;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = folderDialog.SelectedPath;
                dirTreeView.Nodes.Clear();

                try
                {
                    TreeNode rootNode = new TreeNode(Path.GetFileName(selectedPath))
                    {
                        Tag = selectedPath
                    };
                    dirTreeView.Nodes.Add(rootNode);
                    LoadJsonFiles(rootNode, selectedPath);
                    rootNode.Expand();
                    statusTextbox.Text = $"Loaded directory: {selectedPath}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading directory: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadJsonFiles(TreeNode parentNode, string directoryPath)
        {
            foreach (string filePath in Directory.GetFiles(directoryPath, "*.json"))
            {
                TreeNode fileNode = new TreeNode(Path.GetFileName(filePath))
                {
                    Tag = filePath
                };
                parentNode.Nodes.Add(fileNode);
            }

            foreach (string subDirPath in Directory.GetDirectories(directoryPath))
            {
                TreeNode subDirNode = new TreeNode(Path.GetFileName(subDirPath))
                {
                    Tag = subDirPath
                };
                parentNode.Nodes.Add(subDirNode);
                LoadJsonFiles(subDirNode, subDirPath);
            }
        }

        private void DirTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is string filePath &&
                Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                LoadJsonFile(filePath);
            }
        }

        private void LoadJsonFile(string jsonFilePath)
        {
            try
            {
                currentJsonFilePath = jsonFilePath;
                string json = File.ReadAllText(jsonFilePath);
                currentJson = JsonNode.Parse(json);
                PopulateEntityTree(currentJson);
                fileModified = false;
                btn_save.Enabled = false; // No changes yet
                statusTextbox.Text = $"Loaded JSON file: {jsonFilePath}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading JSON file: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Populates the entityTreeView recursively from a JsonNode.
        private void PopulateEntityTree(JsonNode json)
        {
            entityTreeView.Nodes.Clear();
            // Use the filename of the current JSON file if available; otherwise default to "JSON".
            string fileName = string.IsNullOrEmpty(currentJsonFilePath) ? "JSON" : Path.GetFileName(currentJsonFilePath);
            TreeNode root = new TreeNode(fileName)
            {
                Tag = json
            };
            entityTreeView.Nodes.Add(root);
            PopulateTreeRecursive(json, root);
            root.ExpandAll();
            entityTreeView.TopNode = root;
        }

        private void PopulateTreeRecursive(JsonNode node, TreeNode treeNode)
        {
            if (node is JsonObject obj)
            {
                foreach (var kvp in obj)
                {
                    TreeNode child;
                    if (kvp.Value is JsonValue jsonValue)
                    {
                        // Format the value properly based on its type
                        string displayValue = FormatJsonValueForDisplay(jsonValue);
                        child = new TreeNode($"{kvp.Key}: {displayValue}");
                    }
                    else
                    {
                        child = new TreeNode(kvp.Key);
                    }
                    // Set the tooltip using your existing tooltips dictionary.
                    child.ToolTipText = toolTips.tips.ContainsKey(kvp.Key) ? toolTips.tips[kvp.Key] : "";

                    // *** New code: if this property is the SkinID property, try to update its tooltip ***
                    if (kvp.Key.Equals("SkinID (0 - default)", StringComparison.OrdinalIgnoreCase) &&
                        obj.ContainsKey("ShortName"))
                    {
                        string shortName = obj["ShortName"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(shortName) && _itemShortnameToSkinName != null &&
                            _itemShortnameToSkinName.ContainsKey(shortName))
                        {
                            var skinMap = _itemShortnameToSkinName[shortName];
                            if (UInt64.TryParse(kvp.Value?.ToString(), out UInt64 skinId))
                            {
                                // Reverse lookup: find the key (skin display name) whose value equals the skinId.
                                string matchingDisplayName = skinMap.FirstOrDefault(pair => pair.Value == skinId).Key;
                                if (!string.IsNullOrEmpty(matchingDisplayName))
                                {
                                    child.ToolTipText = matchingDisplayName;
                                }
                            }
                        }
                    }
                    // *** End new code ***

                    child.Tag = kvp.Value;
                    treeNode.Nodes.Add(child);
                    PopulateTreeRecursive(kvp.Value, child);
                }
            }
            else if (node is JsonArray arr)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    JsonNode item = arr[i];
                    TreeNode child;
                    if (item is JsonValue jsonValue)
                    {
                        // Format the value properly based on its type
                        string displayValue = FormatJsonValueForDisplay(jsonValue);
                        child = new TreeNode($"[{i}]: {displayValue}");
                    }
                    else
                    {
                        child = new TreeNode($"[{i}]");
                    }
                    child.Tag = item;
                    treeNode.Nodes.Add(child);
                    PopulateTreeRecursive(item, child);
                }
            }
        }

        // Helper method to properly format JSON values for display
        private string FormatJsonValueForDisplay(JsonValue jsonValue)
        {
            try
            {
                // Get the underlying value
                object value = jsonValue.GetValue<object>();

                if (value is double doubleVal)
                    return doubleVal.ToString(CultureInfo.InvariantCulture);
                else if (value is float floatVal)
                    return floatVal.ToString(CultureInfo.InvariantCulture);
                else if (value is bool boolVal)
                    return boolVal.ToString().ToLowerInvariant(); // Display as lowercase true/false
                else if (value is string str)
                    return str; // Return the raw string without quotes.
                else
                    return jsonValue.ToString(); // Default fallback.
            }
            catch
            {
                return jsonValue.ToString();
            }
        }


        private void entityTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                bool enableAddDelButtons = false;
                bool enableImportExportButtons = false;

                // Enable if the selected node's Tag is a JsonArray
                if (e.Node.Tag is JsonArray)
                {
                    enableAddDelButtons = true;
                    enableImportExportButtons = true;
                }
                // Or if the selected node is an item in an array (its parent is a JsonArray)
                else if (e.Node.Parent != null && e.Node.Parent.Tag is JsonArray)
                {
                    enableAddDelButtons = true;
                    enableImportExportButtons = true;
                }

                if (e.Node.Text.Equals("Presets", StringComparison.OrdinalIgnoreCase))
                {
                    enableAddDelButtons = false;
                }

                btn_entity_add.Enabled = enableAddDelButtons;
                btn_entity_del.Enabled = enableAddDelButtons;
                btn_export_entityData.Enabled = enableImportExportButtons;
                btn_import_entityData.Enabled = enableImportExportButtons;
            }
            else
            {
                btn_entity_add.Enabled = false;
                btn_entity_del.Enabled = false;
                btn_export_entityData.Enabled = false;
                btn_import_entityData.Enabled = false;
            }
        }

        // When a tree node is double-clicked, open an edit window for that value.
        private void entityTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null)
                return;

            // Expect node text in the form "Property: Value"
            string[] parts = e.Node.Text.Split(new[] { ':' }, 2);
            if (parts.Length != 2)
                return;

            string propName = parts[0].Trim();
            string currentVal = parts[1].Trim();
            string? skinToolTip = null;
            Type valueType = GetValueType(currentVal);

            // Get any additional combo and skin lists based on the node hierarchy.
            var (comboList, skinList, itemCurrentVal) = GetComboAndSkinLists(e.Node, propName);

            using (EditValueForm editForm = new EditValueForm(propName, currentVal, valueType, comboList, _allItems, skinList))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    object newVal = editForm.NewValue;
                    string displayVal = newVal is bool ? newVal.ToString().ToLower() : newVal.ToString();

                    if (_allItems.ContainsKey(displayVal))
                    {
                        newVal = _allItems[displayVal];
                        displayVal = _allItems[displayVal];
                    }
                    else if (!string.IsNullOrEmpty(itemCurrentVal) && _itemShortnameToSkinName.ContainsKey(itemCurrentVal))
                    {
                        skinToolTip = displayVal;
                        newVal = _itemShortnameToSkinName[itemCurrentVal][displayVal];
                        displayVal = _itemShortnameToSkinName[itemCurrentVal][displayVal].ToString();
                    }

                    if (skinToolTip != null)
                    {
                        e.Node.ToolTipText = skinToolTip;
                    }
                    e.Node.Text = $"{propName}: {displayVal}";

                    if (e.Node.Tag is JsonNode node)
                    {
                        JsonNode newNode = JsonValue.Create(newVal);
                        node.ReplaceWith(newNode);
                        e.Node.Tag = newNode;
                    }

                    // Mark the file as modified.
                    fileModified = true;
                    btn_save.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Determines the type for the current value using simple heuristics.
        /// </summary>
        private Type GetValueType(string currentVal)
        {
            if (bool.TryParse(currentVal, out _))
                return typeof(bool);
            if (int.TryParse(currentVal, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
                return typeof(int);
            if (float.TryParse(currentVal, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out _))
                return typeof(float);
            if (double.TryParse(currentVal, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out _))
                return typeof(double);
            return typeof(string);
        }

        /// <summary>
        /// Retrieves combo list and skin dictionary based on the node's hierarchy and property name.
        /// Returns a tuple of (comboList, skinList, itemCurrentVal).
        /// </summary>
        private (List<string> comboList, Dictionary<string, UInt64> skinList, string itemCurrentVal) GetComboAndSkinLists(TreeNode node, string propName)
        {
            List<string> comboList = null;
            Dictionary<string, UInt64> skinList = null;
            string itemCurrentVal = string.Empty;

            if (node.Parent == null)
                return (comboList, skinList, itemCurrentVal);

            string parentNodeName = node.Parent.Text;
            string grandParentNodeName = node.Parent.Parent?.Text ?? "Root";

            // Mods override.
            if (parentNodeName.Equals("Mods", StringComparison.OrdinalIgnoreCase))
            {
                comboList = _weaponModItems;
            }
            // Check deeper hierarchy.
            else if (node.Parent.Parent != null)
            {
                if (propName.Equals("ShortName", StringComparison.OrdinalIgnoreCase))
                {
                    if (grandParentNodeName.Equals("Wear items", StringComparison.OrdinalIgnoreCase))
                        comboList = _wearItems;
                    else if (grandParentNodeName.Equals("Belt items", StringComparison.OrdinalIgnoreCase))
                        comboList = _beltItems;
                    else if (grandParentNodeName.Equals("List of items", StringComparison.OrdinalIgnoreCase))
                        comboList = new List<string>(_allItems.Keys);
                }
                else if (propName.Equals("SkinID (0 - default)", StringComparison.OrdinalIgnoreCase))
                {
                    // Expect node text in the form "Property: Value" from the first child.
                    if (node.Parent.FirstNode != null)
                    {
                        string[] itemParts = node.Parent.FirstNode.Text.Split(new[] { ':' }, 2);
                        if (itemParts.Length == 2)
                        {
                            itemCurrentVal = itemParts[1].Trim();
                            if (_itemShortnameToSkinName.TryGetValue(itemCurrentVal, out var skinMap) && skinMap.Count > 0)
                            {
                                comboList = new List<string>(skinMap.Keys);
                                skinList = skinMap;
                            }
                        }
                    }
                }
            }

            return (comboList, skinList, itemCurrentVal);
        }



        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentJsonFilePath) || currentJson == null)
            {
                MessageBox.Show("No JSON file loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string newJson = currentJson.ToJsonString(options);
                File.WriteAllText(currentJsonFilePath, newJson);
                statusTextbox.Text = "JSON file saved successfully.";
                //MessageBox.Show("JSON file saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset the modified flag.
                fileModified = false;
                btn_save.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private TreeNode FindNodeByFullPath(TreeNodeCollection nodes, string fullPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.FullPath == fullPath)
                    return node;
                TreeNode found = FindNodeByFullPath(node.Nodes, fullPath);
                if (found != null)
                    return found;
            }
            return null;
        }

        private void btn_entity_add_Click(object sender, EventArgs e)
        {
            TreeNode selected = entityTreeView.SelectedNode;
            if (selected == null)
            {
                MessageBox.Show("Please select a node to add a new item.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            JsonArray targetArray = null;
            bool duplicateExisting = false;

            // If the selected node itself is an array node...
            if (selected.Tag is JsonArray arr)
            {
                targetArray = arr;
                duplicateExisting = false; // We'll add a new blank node
            }
            // Otherwise, if the selected node's parent is an array...
            else if (selected.Parent != null && selected.Parent.Tag is JsonArray arrParent)
            {
                targetArray = arrParent;
                duplicateExisting = true; // We duplicate the selected item
            }
            else
            {
                MessageBox.Show("The selected node is not part of an array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JsonNode newNode;
            if (!duplicateExisting)
            {
                // We're adding a new blank node to the array. 
                // Use the selected node's text as the array name.
                string arrayName = selected.Text.Trim();
                if (arrayName.Equals("Wear items", StringComparison.OrdinalIgnoreCase))
                {
                    var obj = new JsonObject();
                    obj["ShortName"] = "CHANGE ME";
                    obj["SkinID (0 - default)"] = 0;
                    newNode = obj;
                }
                else if (arrayName.Equals("Belt items", StringComparison.OrdinalIgnoreCase))
                {
                    var obj = new JsonObject();
                    obj["ShortName"] = "CHANGE ME";
                    obj["Amount"] = 1;
                    obj["SkinID (0 - default)"] = 0;
                    obj["Mods"] = new JsonArray(); // Empty array
                    obj["Ammo"] = "";
                    newNode = obj;
                }
                else if (arrayName.Equals("List of prefabs", StringComparison.OrdinalIgnoreCase))
                {
                    var obj = new JsonObject();
                    obj["Chance [0.0-100.0]"] = 100.0;
                    obj["The path to the prefab"] = "assets/rust.ai/agents/npcplayer/humannpc/scientist/CHANGEME";
                    newNode = obj;
                }
                else if (arrayName.Equals("List of items", StringComparison.OrdinalIgnoreCase))
                {
                    var obj = new JsonObject();
                    obj["ShortName"] = "CHANGEME";
                    obj["Minimum"] = 1;
                    obj["Maximum"] = 100;
                    obj["Chance [0.0-100.0]"] = 50.0;
                    obj["Is this a blueprint? [true/false]"] = false;
                    obj["SkinID (0 - default)"] = 0;
                    obj["Name (empty - default)"] = "";
                    newNode = obj;
                }
                else if (arrayName.Equals("Mods", StringComparison.OrdinalIgnoreCase))
                {
                    newNode = JsonValue.Create("CHANGE ME");
                }
                else if (arrayName.Equals("Names", StringComparison.OrdinalIgnoreCase))
                {
                    newNode = JsonValue.Create("CHANGE ME");
                }
                else
                {
                    // Default: create an empty string
                    newNode = JsonValue.Create("CHANGE ME");
                }
            }
            else
            {
                // Duplicate the selected item.
                if (!(selected.Tag is JsonNode selectedJsonNode))
                {
                    MessageBox.Show("Selected node does not contain a valid JSON element.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                newNode = selectedJsonNode.DeepClone();
            }

            // Add the new node to the target array.
            targetArray.Add(newNode);

            // Determine the node representing the array.
            // If duplicating, the array node is the selected node's parent; 
            // otherwise it is the selected node.
            TreeNode arrayNode = duplicateExisting ? selected.Parent : selected;

            // Refresh only the children of the array node.
            arrayNode.Nodes.Clear();
            PopulateTreeRecursive((JsonNode)arrayNode.Tag, arrayNode);
            arrayNode.ExpandAll();

            fileModified = true;
            btn_save.Enabled = true;

            // Select the newly added item (the last child).
            if (arrayNode.Nodes.Count > 0)
            {
                TreeNode newSelected = arrayNode.Nodes[arrayNode.Nodes.Count - 1];
                entityTreeView.SelectedNode = newSelected;
                newSelected.EnsureVisible();
                entityTreeView.Focus();
            }
        }


        private void btn_entity_del_Click(object sender, EventArgs e)
        {
            TreeNode selected = entityTreeView.SelectedNode;
            if (selected == null)
            {
                MessageBox.Show("Please select a node to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TreeNode parent = selected.Parent;
            if (parent == null)
            {
                MessageBox.Show("The selected node has no parent (cannot delete).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ensure the parent's Tag is a JsonArray.
            if (!(parent.Tag is JsonArray parentArray))
            {
                MessageBox.Show("The selected node's parent is not an array node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Determine the index of the selected node (assumed to be in the same order as in the array).
            int indexToRemove = selected.Index;
            if (indexToRemove < 0 || indexToRemove >= parentArray.Count)
            {
                MessageBox.Show("Selected node index is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If this is the last item in the array, ask for confirmation.
            if (parentArray.Count == 1)
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to delete the last item in the array?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes)
                {
                    return;
                }
            }

            // Remove the element from the underlying JsonArray.
            parentArray.RemoveAt(indexToRemove);

            // Refresh only the parent's children.
            parent.Nodes.Clear();
            PopulateTreeRecursive((JsonNode)parent.Tag, parent);
            parent.ExpandAll();

            fileModified = true;
            btn_save.Enabled = true;

            // Select the newly last item in the array; if none, select the parent.
            if (parent.Nodes.Count > 0)
            {
                TreeNode newSelected = parent.Nodes[parent.Nodes.Count - 1];
                entityTreeView.SelectedNode = newSelected;
                newSelected.EnsureVisible();
                entityTreeView.Focus();
            }
            else
            {
                entityTreeView.SelectedNode = parent;
                parent.EnsureVisible();
                entityTreeView.Focus();
            }
        }

        // Compare two JsonNode structures (objects, arrays, or values) for basic structural compatibility.
        private bool CompareJsonStructure(JsonNode a, JsonNode b)
        {
            if (a == null || b == null)
                return a == b;

            // Ensure both nodes are of the same concrete type.
            if (a.GetType() != b.GetType())
                return false;

            if (a is JsonObject objA && b is JsonObject objB)
            {
                // They must have the same set of keys.
                if (objA.Count != objB.Count)
                    return false;
                foreach (var kvp in objA)
                {
                    if (!objB.ContainsKey(kvp.Key))
                        return false;
                    if (!CompareJsonStructure(kvp.Value, objB[kvp.Key]))
                        return false;
                }
                return true;
            }
            else if (a is JsonArray arrA && b is JsonArray arrB)
            {
                // If both arrays are empty, we consider them structurally compatible.
                if (arrA.Count == 0 && arrB.Count == 0)
                    return true;
                // Otherwise, compare the structure of their first elements.
                if (arrA.Count > 0 && arrB.Count > 0)
                    return CompareJsonStructure(arrA[0], arrB[0]);
                return false;
            }
            else if (a is JsonValue && b is JsonValue)
            {
                try
                {
                    // Compare the underlying value types.
                    object valA = a.GetValue<object>();
                    object valB = b.GetValue<object>();
                    return valA?.GetType() == valB?.GetType();
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        private void btn_import_entityData_Click(object sender, EventArgs e)
        {
            // Ensure a node is selected.
            TreeNode selected = entityTreeView.SelectedNode;
            if (selected == null)
            {
                MessageBox.Show("Please select a node for importing data.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Open a file picker to select the import file.
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                openDialog.Title = "Import JSON Data";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string importText = File.ReadAllText(openDialog.FileName);
                        JsonNode importJson = JsonNode.Parse(importText);

                        // Determine if we are adding to an array or replacing a child.
                        // If the selected node's Tag is a JsonArray, we add to it.
                        // If the selected node's parent is a JsonArray, we replace the selected node.
                        JsonArray parentArray = null;
                        bool isAddingNewItem = false;
                        if (selected.Tag is JsonArray)
                        {
                            parentArray = (JsonArray)selected.Tag;
                            isAddingNewItem = true;
                        }
                        else if (selected.Parent != null && selected.Parent.Tag is JsonArray)
                        {
                            parentArray = (JsonArray)selected.Parent.Tag;
                            isAddingNewItem = false;
                        }
                        else
                        {
                            MessageBox.Show("The selected node is not part of an array. Import operation cancelled.",
                                "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Determine the target node for structure comparison.
                        // If adding a new item, compare the structure of the array's first item (if any).
                        // Otherwise, compare the selected node's structure.
                        JsonNode targetStructure = null;
                        if (isAddingNewItem)
                        {
                            if (parentArray.Count > 0)
                                targetStructure = parentArray[0];
                            else
                            {
                                // If the array is empty, we assume it's acceptable.
                                targetStructure = importJson;
                            }
                        }
                        else
                        {
                            targetStructure = (JsonNode)selected.Tag;
                        }

                        if (!CompareJsonStructure(importJson, targetStructure))
                        {
                            MessageBox.Show("The imported JSON structure does not match the target structure.",
                                "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        fileModified = true;
                        btn_save.Enabled = true;

                        if (isAddingNewItem)
                        {
                            // Add the imported JSON as a new item to the array.
                            parentArray.Add(importJson);
                            // Find the TreeNode corresponding to the array (selected node).
                            TreeNode arrayNode = selected;
                            // Refresh only this node's children.
                            arrayNode.Nodes.Clear();
                            PopulateTreeRecursive((JsonNode)arrayNode.Tag, arrayNode);
                            arrayNode.ExpandAll();

                            // Select the newly added item (last child).
                            if (arrayNode.Nodes.Count > 0)
                            {
                                TreeNode newNode = arrayNode.Nodes[arrayNode.Nodes.Count - 1];
                                entityTreeView.SelectedNode = newNode;
                                newNode.EnsureVisible();
                                entityTreeView.Focus();
                            }
                        }
                        else
                        {
                            // Replace the selected node's data with the imported JSON.
                            JsonNode oldNode = (JsonNode)selected.Tag;
                            oldNode.ReplaceWith(importJson);
                            selected.Tag = importJson;

                            // Refresh the parent node.
                            TreeNode parentNode = selected.Parent;
                            parentNode.Nodes.Clear();
                            PopulateTreeRecursive((JsonNode)parentNode.Tag, parentNode);
                            parentNode.ExpandAll();

                            // Try to reselect the replaced item (by its index).
                            int index = selected.Index;
                            if (index >= 0 && index < parentNode.Nodes.Count)
                            {
                                TreeNode newNode = parentNode.Nodes[index];
                                entityTreeView.SelectedNode = newNode;
                                newNode.EnsureVisible();
                                entityTreeView.Focus();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error importing JSON file: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_export_entityData_Click(object sender, EventArgs e)
        {
            // Ensure a node is selected and that it contains a JsonNode.
            if (entityTreeView.SelectedNode == null || !(entityTreeView.SelectedNode.Tag is JsonNode selectedJson))
            {
                MessageBox.Show("Please select a node with valid JSON data to export.",
                                "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                saveDialog.Title = "Export JSON Data";
                saveDialog.FileName = "exported.json";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
                            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        };

                        // Export the JSON subtree starting at the selected node.
                        string exportJson = selectedJson.ToJsonString(options);
                        File.WriteAllText(saveDialog.FileName, exportJson);
                        MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting JSON: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(searchText))
            {
                // If search text is empty, restore the full tree view
                if (currentJson != null)
                {
                    PopulateEntityTree(currentJson);
                    statusTextbox.Text = "Search cleared. Displaying all nodes.";
                }
                return;
            }

            if (currentJson == null)
            {
                statusTextbox.Text = "No JSON file loaded to search.";
                return;
            }

            // Clear the tree view before repopulating with filtered results
            entityTreeView.Nodes.Clear();

            // Get the filename for the root node
            string fileName = string.IsNullOrEmpty(currentJsonFilePath) ? "JSON" : Path.GetFileName(currentJsonFilePath);
            TreeNode rootNode = new TreeNode(fileName)
            {
                Tag = currentJson
            };
            entityTreeView.Nodes.Add(rootNode);

            // Use a flag to track if any matches were found
            bool foundMatches = SearchAndPopulateTree(currentJson, rootNode, searchText);

            if (foundMatches)
            {
                rootNode.ExpandAll();
                statusTextbox.Text = $"Search complete. Showing results for: '{searchText}'";
            }
            else
            {
                // No matches found
                PopulateEntityTree(currentJson); // Restore the full tree
                statusTextbox.Text = $"No matches found for: '{searchText}'";
            }
        }

        private bool SearchAndPopulateTree(JsonNode node, TreeNode treeNode, string searchText)
        {
            bool foundMatch = false;

            if (node is JsonObject obj)
            {
                foreach (var kvp in obj)
                {
                    bool keyMatch = kvp.Key.ToLowerInvariant().Contains(searchText);
                    bool valueMatch = false;
                    TreeNode child;

                    if (kvp.Value is JsonValue jsonValue)
                    {
                        string displayValue = FormatJsonValueForDisplay(jsonValue);
                        string valueStr = jsonValue.ToString().ToLowerInvariant();
                        valueMatch = valueStr.Contains(searchText);
                        child = new TreeNode($"{kvp.Key}: {displayValue}");
                    }
                    else
                    {
                        child = new TreeNode(kvp.Key);
                    }

                    child.ToolTipText = toolTips.tips.ContainsKey(kvp.Key) ? toolTips.tips[kvp.Key] : "";
                    child.Tag = kvp.Value;

                    if (keyMatch || valueMatch)
                    {
                        // Direct match found:
                        // Add the full subtree without filtering.
                        PopulateTreeRecursive(kvp.Value, child);
                        treeNode.Nodes.Add(child);
                        foundMatch = true;
                    }
                    else
                    {
                        // No direct match: continue filtering the children.
                        bool childrenMatch = SearchAndPopulateTree(kvp.Value, child, searchText);
                        if (childrenMatch)
                        {
                            treeNode.Nodes.Add(child);
                            foundMatch = true;
                        }
                    }
                }
            }
            else if (node is JsonArray arr)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    JsonNode item = arr[i];
                    TreeNode child;

                    if (item is JsonValue jsonValue)
                    {
                        string displayValue = FormatJsonValueForDisplay(jsonValue);
                        string valueStr = jsonValue.ToString().ToLowerInvariant();
                        bool valueMatch = valueStr.Contains(searchText);
                        child = new TreeNode($"[{i}]: {displayValue}");

                        if (valueMatch)
                        {
                            child.BackColor = Color.LightYellow;
                            treeNode.Nodes.Add(child);
                            foundMatch = true;
                        }
                    }
                    else
                    {
                        child = new TreeNode($"[{i}]");
                        child.Tag = item;
                        bool childrenMatch = SearchAndPopulateTree(item, child, searchText);
                        if (childrenMatch)
                        {
                            treeNode.Nodes.Add(child);
                            foundMatch = true;
                        }
                    }
                }
            }
            return foundMatch;
        }

        private void btn_Search_Clear_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            btn_Search_Click(sender, e);
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Prevent the beep sound
                e.SuppressKeyPress = true;

                // Trigger the search button click
                btn_Search_Click(sender, EventArgs.Empty);
            }
        }
    }
}
