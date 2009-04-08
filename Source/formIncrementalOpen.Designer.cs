namespace IncrementalOpener
{
    partial class formIncrementalOpen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formIncrementalOpen));
            this.listFiles = new System.Windows.Forms.ListView();
            this.File = new System.Windows.Forms.ColumnHeader();
            this.Path = new System.Windows.Forms.ColumnHeader();
            this.Project = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textFileName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextOpenContainingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listFiles
            // 
            this.listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.File,
            this.Path,
            this.Project});
            this.listFiles.ContextMenuStrip = this.contextMenuStrip1;
            this.listFiles.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listFiles.FullRowSelect = true;
            this.listFiles.GridLines = true;
            this.listFiles.Location = new System.Drawing.Point(8, 8);
            this.listFiles.MultiSelect = false;
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(832, 430);
            this.listFiles.SmallImageList = this.imageList1;
            this.listFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listFiles.TabIndex = 1;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.Details;
            this.listFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFiles_MouseDoubleClick);
            this.listFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listFiles_ColumnClick);
            this.listFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listFiles_MouseDown);
            this.listFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listFiles_ItemSelectionChanged);
            // 
            // File
            // 
            this.File.Text = "File";
            this.File.Width = 200;
            // 
            // Path
            // 
            this.Path.Text = "Path";
            this.Path.Width = 400;
            // 
            // Project
            // 
            this.Project.Text = "Project";
            this.Project.Width = 200;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "script.ico");
            // 
            // textFileName
            // 
            this.textFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textFileName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFileName.Location = new System.Drawing.Point(9, 449);
            this.textFileName.Name = "textFileName";
            this.textFileName.Size = new System.Drawing.Size(831, 21);
            this.textFileName.TabIndex = 0;
            this.textFileName.TextChanged += new System.EventHandler(this.textFileName_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextOpenContainingFolder});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 48);
            // 
            // contextOpenContainingFolder
            // 
            this.contextOpenContainingFolder.Name = "contextOpenContainingFolder";
            this.contextOpenContainingFolder.Size = new System.Drawing.Size(183, 22);
            this.contextOpenContainingFolder.Text = "Open containing folder";
            this.contextOpenContainingFolder.Click += new System.EventHandler(this.contextOpenContainingFolder_Click);
            // 
            // formIncrementalOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 475);
            this.Controls.Add(this.textFileName);
            this.Controls.Add(this.listFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formIncrementalOpen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open file";
            this.Shown += new System.EventHandler(this.formIncrementalOpen_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formIncrementalOpen_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.ColumnHeader File;
        private System.Windows.Forms.ColumnHeader Path;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader Project;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem contextOpenContainingFolder;
    }
}