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
            searchTextBox = new TextBox();
            btn_Search = new Button();
            btn_Search_Clear = new Button();
            SuspendLayout();
            // 
            // btn_load
            // 
            btn_load.Location = new Point(15, 9);
            btn_load.Margin = new Padding(3, 2, 3, 2);
            btn_load.Name = "btn_load";
            btn_load.Size = new Size(118, 22);
            btn_load.TabIndex = 0;
            btn_load.Text = "Load Directory";
            btn_load.UseVisualStyleBackColor = true;
            btn_load.Click += btn_load_Click;
            // 
            // btn_save
            // 
            btn_save.Enabled = false;
            btn_save.Location = new Point(187, 9);
            btn_save.Margin = new Padding(3, 2, 3, 2);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(81, 22);
            btn_save.TabIndex = 1;
            btn_save.Text = "Save File";
            btn_save.UseVisualStyleBackColor = true;
            btn_save.Click += btn_save_Click;
            // 
            // dirTreeView
            // 
            dirTreeView.Location = new Point(15, 40);
            dirTreeView.Margin = new Padding(3, 2, 3, 2);
            dirTreeView.Name = "dirTreeView";
            dirTreeView.Size = new Size(254, 623);
            dirTreeView.TabIndex = 2;
            // 
            // entityTreeView
            // 
            entityTreeView.Location = new Point(274, 40);
            entityTreeView.Margin = new Padding(3, 2, 3, 2);
            entityTreeView.Name = "entityTreeView";
            entityTreeView.Size = new Size(738, 623);
            entityTreeView.TabIndex = 3;
            // 
            // btn_entity_del
            // 
            btn_entity_del.Enabled = false;
            btn_entity_del.Location = new Point(320, 9);
            btn_entity_del.Margin = new Padding(3, 2, 3, 2);
            btn_entity_del.Name = "btn_entity_del";
            btn_entity_del.Size = new Size(41, 22);
            btn_entity_del.TabIndex = 8;
            btn_entity_del.Text = "Del";
            btn_entity_del.UseVisualStyleBackColor = true;
            btn_entity_del.Click += btn_entity_del_Click;
            // 
            // btn_entity_add
            // 
            btn_entity_add.Enabled = false;
            btn_entity_add.Location = new Point(274, 9);
            btn_entity_add.Margin = new Padding(3, 2, 3, 2);
            btn_entity_add.Name = "btn_entity_add";
            btn_entity_add.Size = new Size(40, 22);
            btn_entity_add.TabIndex = 7;
            btn_entity_add.Text = "Add";
            btn_entity_add.UseVisualStyleBackColor = true;
            btn_entity_add.Click += btn_entity_add_Click;
            // 
            // btn_import_entityData
            // 
            btn_import_entityData.Enabled = false;
            btn_import_entityData.Location = new Point(366, 9);
            btn_import_entityData.Margin = new Padding(3, 2, 3, 2);
            btn_import_entityData.Name = "btn_import_entityData";
            btn_import_entityData.Size = new Size(68, 22);
            btn_import_entityData.TabIndex = 9;
            btn_import_entityData.Text = "Import";
            btn_import_entityData.UseVisualStyleBackColor = true;
            btn_import_entityData.Click += btn_import_entityData_Click;
            // 
            // btn_export_entityData
            // 
            btn_export_entityData.Enabled = false;
            btn_export_entityData.Location = new Point(440, 9);
            btn_export_entityData.Margin = new Padding(3, 2, 3, 2);
            btn_export_entityData.Name = "btn_export_entityData";
            btn_export_entityData.Size = new Size(65, 22);
            btn_export_entityData.TabIndex = 10;
            btn_export_entityData.Text = "Export";
            btn_export_entityData.UseVisualStyleBackColor = true;
            btn_export_entityData.Click += btn_export_entityData_Click;
            // 
            // statusTextbox
            // 
            statusTextbox.BorderStyle = BorderStyle.FixedSingle;
            statusTextbox.ImeMode = ImeMode.NoControl;
            statusTextbox.Location = new Point(15, 667);
            statusTextbox.Margin = new Padding(3, 2, 3, 2);
            statusTextbox.Name = "statusTextbox";
            statusTextbox.ReadOnly = true;
            statusTextbox.Size = new Size(997, 23);
            statusTextbox.TabIndex = 11;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(725, 8);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(287, 23);
            searchTextBox.TabIndex = 12;
            // 
            // btn_Search
            // 
            btn_Search.Location = new Point(605, 7);
            btn_Search.Margin = new Padding(3, 2, 3, 2);
            btn_Search.Name = "btn_Search";
            btn_Search.Size = new Size(65, 22);
            btn_Search.TabIndex = 13;
            btn_Search.Text = "Search";
            btn_Search.UseVisualStyleBackColor = true;
            btn_Search.Click += btn_Search_Click;
            // 
            // btn_Search_Clear
            // 
            btn_Search_Clear.Location = new Point(676, 7);
            btn_Search_Clear.Margin = new Padding(3, 2, 3, 2);
            btn_Search_Clear.Name = "btn_Search_Clear";
            btn_Search_Clear.Size = new Size(43, 22);
            btn_Search_Clear.TabIndex = 14;
            btn_Search_Clear.Text = "Clear";
            btn_Search_Clear.UseVisualStyleBackColor = true;
            btn_Search_Clear.Click += btn_Search_Clear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1028, 696);
            Controls.Add(btn_Search_Clear);
            Controls.Add(btn_Search);
            Controls.Add(searchTextBox);
            Controls.Add(statusTextbox);
            Controls.Add(btn_export_entityData);
            Controls.Add(btn_import_entityData);
            Controls.Add(btn_entity_del);
            Controls.Add(btn_entity_add);
            Controls.Add(entityTreeView);
            Controls.Add(dirTreeView);
            Controls.Add(btn_save);
            Controls.Add(btn_load);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "BetterNPC";
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
        private TextBox searchTextBox;
        private Button btn_Search;
        private Button btn_Search_Clear;
    }
}
