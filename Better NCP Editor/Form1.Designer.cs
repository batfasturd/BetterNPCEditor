namespace Better_NCP_Editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_load = new Button();
            btn_save = new Button();
            dirTreeView = new TreeView();
            entityTreeView = new TreeView();
            btn_entity_del = new Button();
            btn_entity_add = new Button();
            btn_import_entityData = new Button();
            btn_export_entityData = new Button();
            statusTextbox = new TextBox();
            SuspendLayout();
            // 
            // btn_load
            // 
            btn_load.Location = new Point(17, 12);
            btn_load.Name = "btn_load";
            btn_load.Size = new Size(135, 29);
            btn_load.TabIndex = 0;
            btn_load.Text = "Load Directory";
            btn_load.UseVisualStyleBackColor = true;
            btn_load.Click += btn_load_Click;
            // 
            // btn_save
            // 
            btn_save.Enabled = false;
            btn_save.Location = new Point(214, 12);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(93, 29);
            btn_save.TabIndex = 1;
            btn_save.Text = "Save File";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // dirTreeView
            // 
            dirTreeView.Location = new Point(17, 53);
            dirTreeView.Name = "dirTreeView";
            dirTreeView.Size = new Size(290, 812);
            dirTreeView.TabIndex = 2;
            // 
            // entityTreeView
            // 
            entityTreeView.Location = new Point(313, 53);
            entityTreeView.Name = "entityTreeView";
            entityTreeView.Size = new Size(843, 812);
            entityTreeView.TabIndex = 3;
            // 
            // btn_entity_del
            // 
            btn_entity_del.Enabled = false;
            btn_entity_del.Location = new Point(366, 12);
            btn_entity_del.Name = "btn_entity_del";
            btn_entity_del.Size = new Size(47, 29);
            btn_entity_del.TabIndex = 8;
            btn_entity_del.Text = "Del";
            btn_entity_del.UseVisualStyleBackColor = true;
            btn_entity_del.Click += btn_entity_del_Click;
            // 
            // btn_entity_add
            // 
            btn_entity_add.Enabled = false;
            btn_entity_add.Location = new Point(313, 12);
            btn_entity_add.Name = "btn_entity_add";
            btn_entity_add.Size = new Size(46, 29);
            btn_entity_add.TabIndex = 7;
            btn_entity_add.Text = "Add";
            btn_entity_add.UseVisualStyleBackColor = true;
            btn_entity_add.Click += btn_entity_add_Click;
            // 
            // btn_import_entityData
            // 
            btn_import_entityData.Enabled = false;
            btn_import_entityData.Location = new Point(493, 12);
            btn_import_entityData.Name = "btn_import_entityData";
            btn_import_entityData.Size = new Size(70, 29);
            btn_import_entityData.TabIndex = 9;
            btn_import_entityData.Text = "Import";
            btn_import_entityData.UseVisualStyleBackColor = true;
            btn_import_entityData.Click += btn_import_entityData_Click;
            // 
            // btn_export_entityData
            // 
            btn_export_entityData.Enabled = false;
            btn_export_entityData.Location = new Point(569, 12);
            btn_export_entityData.Name = "btn_export_entityData";
            btn_export_entityData.Size = new Size(66, 29);
            btn_export_entityData.TabIndex = 10;
            btn_export_entityData.Text = "Export";
            btn_export_entityData.UseVisualStyleBackColor = true;
            btn_export_entityData.Click += btn_export_entityData_Click;
            // 
            // statusTextbox
            // 
            statusTextbox.BorderStyle = BorderStyle.FixedSingle;
            statusTextbox.ImeMode = ImeMode.NoControl;
            statusTextbox.Location = new Point(660, 14);
            statusTextbox.Name = "statusTextbox";
            statusTextbox.ReadOnly = true;
            statusTextbox.Size = new Size(496, 27);
            statusTextbox.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1176, 881);
            Controls.Add(statusTextbox);
            Controls.Add(btn_export_entityData);
            Controls.Add(btn_import_entityData);
            Controls.Add(btn_entity_del);
            Controls.Add(btn_entity_add);
            Controls.Add(entityTreeView);
            Controls.Add(dirTreeView);
            Controls.Add(btn_save);
            Controls.Add(btn_load);
            Name = "Form1";
            Text = "BetterNPC Editor V1.0";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_load;
        private Button btn_save;
        private TreeView dirTreeView;
        private TreeView entityTreeView;
        private Button btn_entity_del;
        private Button btn_entity_add;
        private Button btn_import_entityData;
        private Button btn_export_entityData;
        private TextBox statusTextbox;
    }
}
