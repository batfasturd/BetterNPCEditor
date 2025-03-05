using System;
using System.Drawing;
using System.Windows.Forms;

namespace Better_NCP_Editor
{
    public partial class EditValueForm : Form
    {
        public object NewValue { get; private set; }
        private Control inputControl;

        public EditValueForm(string propertyName, string currentValue, Type valueType)
        {
            // Set the minimum size.
            this.MinimumSize = new Size(210, 150);  // Example: 300x150 pixels
                                                    // Optionally, set the default size if you want:
            this.ClientSize = new Size(210, 150);
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
                inputWidth = TextRenderer.MeasureText(currentValue, this.Font).Width + 20;
            }

            // Determine the desired width: the maximum of the key width and input width, plus extra padding.
            int desiredWidth = Math.Max(keySize.Width, inputWidth) + 40;

            // Set the form's client size (you can also set MinimumSize here if needed).
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

            // Create the input control with width based on desiredWidth minus margins.
            int controlWidth = desiredWidth - 20;
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
                Location = new Point(100, 80),  // Positioned to the right of OK.
                Size = new Size(80, 25),
                DialogResult = DialogResult.Cancel,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            this.Controls.Add(btnCancel);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // Determine new value based on input control.
            if (inputControl is ComboBox combo)
            {
                NewValue = combo.SelectedItem.ToString() == "true";
            }
            else if (inputControl is TextBox txt)
            {
                // Try to parse an integer; if it fails, treat it as a string.
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
