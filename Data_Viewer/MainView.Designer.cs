namespace Data_Viewer
{
    partial class MainView
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savetxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadtxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smashBrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.starTreckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waysideScholToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewer = new Data_Viewer.Views.BinTreeViewer();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(692, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savetxtToolStripMenuItem,
            this.loadtxtToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // savetxtToolStripMenuItem
            // 
            this.savetxtToolStripMenuItem.Name = "savetxtToolStripMenuItem";
            this.savetxtToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.savetxtToolStripMenuItem.Text = "Save (*.txt) ...";
            // 
            // loadtxtToolStripMenuItem
            // 
            this.loadtxtToolStripMenuItem.Name = "loadtxtToolStripMenuItem";
            this.loadtxtToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.loadtxtToolStripMenuItem.Text = "Load (*.txt) ...";
            // 
            // testDataToolStripMenuItem
            // 
            this.testDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smashBrosToolStripMenuItem,
            this.starTreckToolStripMenuItem,
            this.waysideScholToolStripMenuItem});
            this.testDataToolStripMenuItem.Name = "testDataToolStripMenuItem";
            this.testDataToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.testDataToolStripMenuItem.Text = "Test Data";
            // 
            // smashBrosToolStripMenuItem
            // 
            this.smashBrosToolStripMenuItem.Name = "smashBrosToolStripMenuItem";
            this.smashBrosToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.smashBrosToolStripMenuItem.Text = "Smash Bros.";
            this.smashBrosToolStripMenuItem.Click += new System.EventHandler(this.smashBrosToolStripMenuItem_Click);
            // 
            // starTreckToolStripMenuItem
            // 
            this.starTreckToolStripMenuItem.Name = "starTreckToolStripMenuItem";
            this.starTreckToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.starTreckToolStripMenuItem.Text = "Star Treck";
            this.starTreckToolStripMenuItem.Click += new System.EventHandler(this.starTreckToolStripMenuItem_Click);
            // 
            // waysideScholToolStripMenuItem
            // 
            this.waysideScholToolStripMenuItem.Name = "waysideScholToolStripMenuItem";
            this.waysideScholToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.waysideScholToolStripMenuItem.Text = "Wayside School";
            this.waysideScholToolStripMenuItem.Click += new System.EventHandler(this.waysideScholToolStripMenuItem_Click);
            // 
            // treeViewer
            // 
            this.treeViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewer.Location = new System.Drawing.Point(0, 24);
            this.treeViewer.MinimumSize = new System.Drawing.Size(600, 400);
            this.treeViewer.Name = "treeViewer";
            this.treeViewer.Size = new System.Drawing.Size(692, 467);
            this.treeViewer.TabIndex = 0;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 491);
            this.Controls.Add(this.treeViewer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 525);
            this.Name = "MainView";
            this.Text = "MainView";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Views.BinTreeViewer treeViewer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savetxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadtxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smashBrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem starTreckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waysideScholToolStripMenuItem;
    }
}