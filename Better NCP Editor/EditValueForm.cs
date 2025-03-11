using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Better_NCP_Editor
{
    public partial class EditValueForm : Form
    {
        public object NewValue { get; private set; }
        private Control inputControl;
        private readonly Type valueType;

        public EditValueForm(string propertyName, string currentValue, Type valueType, List<string>? comboItems, Dictionary<string, string> allItems, Dictionary<string, UInt64> skinIDs)
        {
            // Validate required parameters.
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property name cannot be null or empty.", nameof(propertyName));
            if (currentValue == null)
                throw new ArgumentNullException(nameof(currentValue));
            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType));

            this.valueType = valueType;

            // Set up the form's basic properties and size.
            InitializeForm(propertyName, currentValue, comboItems);
            // Create and add the input control.
            CreateInputControl(propertyName, currentValue, comboItems, allItems, skinIDs);
            // Create and add OK/Cancel buttons.
            CreateButtons();
        }

        private void InitializeForm(string propertyName, string currentValue, List<string>? comboItems)
        {
            this.MinimumSize = new Size(210, 120);
            this.AutoScaleMode = AutoScaleMode.None;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = $"Edit {propertyName}";

            // Measure text widths for layout.
            int propertyNameWidth = TextRenderer.MeasureText(propertyName, this.Font).Width;
            int currentValueWidth = valueType == typeof(bool)
                ? TextRenderer.MeasureText("false", this.Font).Width + 30
                : TextRenderer.MeasureText(currentValue, this.Font).Width + 40;

            int comboItemsWidth = 0;
            if (comboItems != null)
            {
                foreach (var item in comboItems)
                {
                    int itemWidth = TextRenderer.MeasureText(item, this.Font).Width;
                    comboItemsWidth = Math.Max(comboItemsWidth, itemWidth);
                }
                comboItemsWidth += 20; // extra padding for dropdown arrow and margin.
            }

            int desiredWidth = Math.Max(propertyNameWidth, Math.Max(currentValueWidth, comboItemsWidth)) + 40;
            this.ClientSize = new Size(desiredWidth, 120);

            // Create and add the property label.
            Label lblProperty = new Label()
            {
                Text = propertyName,
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(lblProperty);
        }

        private void CreateInputControl(string propertyName, string currentValue, List<string>? comboItems, Dictionary<string, string> allItems, Dictionary<string, UInt64> skinIDs)
        {
            int controlWidth = this.ClientSize.Width - 20;

            if (comboItems != null)
            {
                // Create a ComboBox when comboItems are provided.
                ComboBox combo = new ComboBox
                {
                    Location = new Point(10, 40),
                    Width = controlWidth,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                // Add items and adjust the dropdown width to fit the widest item.
                int maxItemWidth = comboItems.Select(item => TextRenderer.MeasureText(item, combo.Font).Width).Max();
                int paddedWidth = maxItemWidth + 20;
                combo.DropDownWidth = paddedWidth;
                if (combo.Width < paddedWidth)
                    combo.Width = paddedWidth;

                foreach (var item in comboItems)
                {
                    combo.Items.Add(item);
                }

                // Attempt to set the selected item using the provided dictionaries.
                string? selectedKey = allItems.FirstOrDefault(x => x.Value == currentValue).Key;

                // If not found, try using skinIDs (with safe parsing).
                if (string.IsNullOrEmpty(selectedKey) && skinIDs != null && UInt64.TryParse(currentValue, out UInt64 parsedValue))
                {
                    selectedKey = skinIDs.FirstOrDefault(x => x.Value == parsedValue).Key;
                }

                if (!string.IsNullOrEmpty(selectedKey) && combo.Items.Contains(selectedKey))
                {
                    combo.SelectedItem = selectedKey;
                }
                else if (combo.Items.Count > 0)
                {
                    combo.SelectedIndex = 0;
                }
                inputControl = combo;
                this.Controls.Add(combo);
            }
            else
            {
                // For bool types, use a ComboBox to choose true/false.
                if (valueType == typeof(bool))
                {
                    ComboBox combo = new ComboBox
                    {
                        Location = new Point(10, 40),
                        Width = controlWidth,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    combo.Items.Add("true");
                    combo.Items.Add("false");
                    // Parse the current value safely.
                    if (bool.TryParse(currentValue, out bool boolVal))
                    {
                        combo.SelectedItem = boolVal.ToString().ToLower();
                    }
                    else
                    {
                        combo.SelectedIndex = 0;
                    }
                    inputControl = combo;
                    this.Controls.Add(combo);
                }
                else
                {
                    // Default to a TextBox for other types.
                    TextBox txtBox = new TextBox
                    {
                        Location = new Point(10, 40),
                        Width = controlWidth,
                        Text = currentValue
                    };
                    inputControl = txtBox;
                    this.Controls.Add(txtBox);
                }
            }
        }

        private void CreateButtons()
        {
            // OK button.
            Button btnOk = new Button
            {
                Text = "OK",
                Location = new Point(10, 80),
                Size = new Size(80, 25),
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);

            // Cancel button.
            Button btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(100, 80),
                Size = new Size(80, 25),
                DialogResult = DialogResult.Cancel,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // Determine the new value based on the type of the input control.
            if (inputControl is ComboBox combo)
            {
                if (valueType == typeof(bool))
                {
                    // Parse the selected string to a boolean.
                    if (bool.TryParse(combo.SelectedItem?.ToString(), out bool boolVal))
                    {
                        NewValue = boolVal;
                    }
                    else
                    {
                        // Handle parsing error as needed.
                        NewValue = false;
                    }
                }
                else
                {
                    // For non-boolean values, return the selected string.
                    NewValue = combo.SelectedItem?.ToString() ?? string.Empty;
                }
            }
            else if (inputControl is TextBox txt)
            {
                // If possible, parse to int; otherwise, leave as string.
                if (int.TryParse(txt.Text, out int intValue))
                    NewValue = intValue;
                else
                    NewValue = txt.Text;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
