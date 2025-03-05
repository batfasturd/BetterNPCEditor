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
            // Set form properties.
            this.Text = $"Edit {propertyName}";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(250, 120);

            // Label for the property.
            Label lblProperty = new Label()
            {
                Text = propertyName,
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(lblProperty);

            // Create the appropriate input control.
            if (valueType == typeof(bool))
            {
                ComboBox combo = new ComboBox()
                {
                    Location = new Point(10, 40),
                    Width = 200,
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
                    Width = 200,
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
                DialogResult = DialogResult.OK
            };
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);

            // Cancel button.
            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(120, 80),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
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
