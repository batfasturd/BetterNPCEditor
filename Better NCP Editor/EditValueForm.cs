using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Better_NCP_Editor
{
    public partial class EditValueForm : Form
    {
        public object NewValue { get; private set; }
        private Control inputControl;

        // Constructor now takes an extra parameter: parentNodeName.
        public EditValueForm(string propertyName, string currentValue, Type valueType, List<string>? comboItems,Dictionary<String,String> allItems)
        {
            // Set the minimum and default size.
            this.MinimumSize = new Size(210, 120);
            //this.AutoScaleDimensions = new SizeF(96F, 96F); // or your base DPI
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Measure the width of the property name.
            Size keySize = TextRenderer.MeasureText(propertyName, this.Font);

            // Determine the width required for the input control.
            int inputWidth = 0;
            if (valueType == typeof(bool))
            {
                // For a boolean, we'll use the longer of "true" or "false" plus padding.
                inputWidth = TextRenderer.MeasureText("false", this.Font).Width + 30;
            }
            else
            {
                inputWidth = TextRenderer.MeasureText(currentValue, this.Font).Width + 40;
            }

            // Determine the desired width: the maximum of the key width and input width, plus extra padding.
            int desiredWidth = Math.Max(keySize.Width, inputWidth) + 40;
            this.ClientSize = new Size(desiredWidth, 120);

            this.Text = $"Edit {propertyName}";
            this.StartPosition = FormStartPosition.CenterParent;

            // Create the property label.
            Label lblProperty = new Label()
            {
                Text = propertyName,
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(lblProperty);

            int controlWidth = desiredWidth - 20;

            // Check if we need to use a special ComboBox (for "ShortName" in "Wear items").
            if (comboItems != null)
            {
                ComboBox combo = new ComboBox()
                {
                    Location = new Point(10, 40),
                    Width = controlWidth,  // initial width based on desiredWidth minus margins
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                int maxWidth = 0;
                foreach (var key in comboItems)
                {
                    combo.Items.Add(key);
                    // Measure each item's width using the ComboBox's font.
                    Size itemSize = TextRenderer.MeasureText(key, combo.Font);
                    if (itemSize.Width > maxWidth)
                        maxWidth = itemSize.Width;
                }

                // Optionally add some extra padding.
                int paddedWidth = maxWidth + 20;

                // Set the DropDownWidth to ensure items are fully visible.
                combo.DropDownWidth = paddedWidth;

                // Optionally, you can also adjust the combo's Width if you want it to match the dropdown width.
                if (combo.Width < paddedWidth)
                    combo.Width = paddedWidth;

                // Set selected item to currentValue if found, otherwise select the first item.
                var matchingPair = allItems.FirstOrDefault(x => x.Value == currentValue);
                if (matchingPair.Key != null)
                {
                    combo.SelectedItem = matchingPair.Key;
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
                // Create the input control normally.
                if (valueType == typeof(bool))
                {
                    ComboBox combo = new ComboBox()
                    {
                        Location = new Point(10, 40),
                        Width = controlWidth,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    combo.Items.Add("true");
                    combo.Items.Add("false");
                    combo.SelectedItem = currentValue;
                    inputControl = combo;
                    this.Controls.Add(combo);
                }
                else
                {
                    TextBox txtBox = new TextBox()
                    {
                        Location = new Point(10, 40),
                        Width = controlWidth,
                        Text = currentValue
                    };
                    inputControl = txtBox;
                    this.Controls.Add(txtBox);
                }
            }

            // OK button.
            Button btnOk = new Button()
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
            Button btnCancel = new Button()
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
            // If the input control is the special ComboBox, return the dictionary value.
            if (inputControl is ComboBox combo && combo.DropDownStyle == ComboBoxStyle.DropDownList &&
                combo.Items.Count > 0)
            {
                NewValue = combo.SelectedItem.ToString();
            }
            else if (inputControl is ComboBox comboBool)
            {
                NewValue = comboBool.SelectedItem.ToString() == "true";
            }
            else if (inputControl is TextBox txt)
            {
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
