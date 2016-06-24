namespace Data_Viewer.Views
{
    partial class BinTreeViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinTreeViewer));
            this.txtInsert = new System.Windows.Forms.TextBox();
            this.gboInsert = new System.Windows.Forms.GroupBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.gboImpl = new System.Windows.Forms.GroupBox();
            this.rdoSplay = new System.Windows.Forms.RadioButton();
            this.rdoRedBlack = new System.Windows.Forms.RadioButton();
            this.rdoAVL = new System.Windows.Forms.RadioButton();
            this.rdoBasic = new System.Windows.Forms.RadioButton();
            this.gboList = new System.Windows.Forms.GroupBox();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.gboTree = new System.Windows.Forms.GroupBox();
            this.lblStats = new System.Windows.Forms.Label();
            this.treeMainView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gboActions = new System.Windows.Forms.GroupBox();
            this.btnGetMin = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gboInsert.SuspendLayout();
            this.gboImpl.SuspendLayout();
            this.gboList.SuspendLayout();
            this.gboTree.SuspendLayout();
            this.gboActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInsert
            // 
            this.txtInsert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInsert.Location = new System.Drawing.Point(6, 21);
            this.txtInsert.Name = "txtInsert";
            this.txtInsert.Size = new System.Drawing.Size(476, 20);
            this.txtInsert.TabIndex = 0;
            // 
            // gboInsert
            // 
            this.gboInsert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboInsert.Controls.Add(this.btnInsert);
            this.gboInsert.Controls.Add(this.txtInsert);
            this.gboInsert.Location = new System.Drawing.Point(3, 3);
            this.gboInsert.Name = "gboInsert";
            this.gboInsert.Size = new System.Drawing.Size(594, 57);
            this.gboInsert.TabIndex = 1;
            this.gboInsert.TabStop = false;
            this.gboInsert.Text = "Insert Items:";
            // 
            // btnInsert
            // 
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.Location = new System.Drawing.Point(488, 19);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(100, 23);
            this.btnInsert.TabIndex = 1;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // gboImpl
            // 
            this.gboImpl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboImpl.Controls.Add(this.rdoSplay);
            this.gboImpl.Controls.Add(this.rdoRedBlack);
            this.gboImpl.Controls.Add(this.rdoAVL);
            this.gboImpl.Controls.Add(this.rdoBasic);
            this.gboImpl.Location = new System.Drawing.Point(3, 66);
            this.gboImpl.Name = "gboImpl";
            this.gboImpl.Size = new System.Drawing.Size(594, 45);
            this.gboImpl.TabIndex = 2;
            this.gboImpl.TabStop = false;
            this.gboImpl.Text = "Tree Implementaiton:";
            // 
            // rdoSplay
            // 
            this.rdoSplay.AutoSize = true;
            this.rdoSplay.Location = new System.Drawing.Point(250, 19);
            this.rdoSplay.Name = "rdoSplay";
            this.rdoSplay.Size = new System.Drawing.Size(51, 17);
            this.rdoSplay.TabIndex = 3;
            this.rdoSplay.Text = "Splay";
            this.rdoSplay.UseVisualStyleBackColor = true;
            this.rdoSplay.Click += new System.EventHandler(this.rdoAny_Click);
            // 
            // rdoRedBlack
            // 
            this.rdoRedBlack.AutoSize = true;
            this.rdoRedBlack.Location = new System.Drawing.Point(153, 19);
            this.rdoRedBlack.Name = "rdoRedBlack";
            this.rdoRedBlack.Size = new System.Drawing.Size(75, 17);
            this.rdoRedBlack.TabIndex = 2;
            this.rdoRedBlack.Text = "Red-Black";
            this.rdoRedBlack.UseVisualStyleBackColor = true;
            this.rdoRedBlack.Click += new System.EventHandler(this.rdoAny_Click);
            // 
            // rdoAVL
            // 
            this.rdoAVL.AutoSize = true;
            this.rdoAVL.Location = new System.Drawing.Point(89, 19);
            this.rdoAVL.Name = "rdoAVL";
            this.rdoAVL.Size = new System.Drawing.Size(45, 17);
            this.rdoAVL.TabIndex = 1;
            this.rdoAVL.Text = "AVL";
            this.rdoAVL.UseVisualStyleBackColor = true;
            this.rdoAVL.Click += new System.EventHandler(this.rdoAny_Click);
            // 
            // rdoBasic
            // 
            this.rdoBasic.AutoSize = true;
            this.rdoBasic.Checked = true;
            this.rdoBasic.Location = new System.Drawing.Point(17, 19);
            this.rdoBasic.Name = "rdoBasic";
            this.rdoBasic.Size = new System.Drawing.Size(51, 17);
            this.rdoBasic.TabIndex = 0;
            this.rdoBasic.TabStop = true;
            this.rdoBasic.Text = "Basic";
            this.rdoBasic.UseVisualStyleBackColor = true;
            this.rdoBasic.Click += new System.EventHandler(this.rdoAny_Click);
            // 
            // gboList
            // 
            this.gboList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboList.Controls.Add(this.lstItems);
            this.gboList.Controls.Add(this.cboSort);
            this.gboList.Location = new System.Drawing.Point(322, 117);
            this.gboList.Name = "gboList";
            this.gboList.Size = new System.Drawing.Size(275, 221);
            this.gboList.TabIndex = 3;
            this.gboList.TabStop = false;
            this.gboList.Text = "List View:";
            // 
            // lstItems
            // 
            this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItems.FormattingEnabled = true;
            this.lstItems.IntegralHeight = false;
            this.lstItems.Location = new System.Drawing.Point(6, 46);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(263, 169);
            this.lstItems.TabIndex = 1;
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
            // 
            // cboSort
            // 
            this.cboSort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.FormattingEnabled = true;
            this.cboSort.Items.AddRange(new object[] {
            "PreOrder",
            "InOrder",
            "PostOrder",
            "LevelOrder"});
            this.cboSort.Location = new System.Drawing.Point(6, 19);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(263, 21);
            this.cboSort.TabIndex = 0;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.cboSort_SelectedIndexChanged);
            // 
            // gboTree
            // 
            this.gboTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboTree.Controls.Add(this.lblStats);
            this.gboTree.Controls.Add(this.treeMainView);
            this.gboTree.Location = new System.Drawing.Point(3, 117);
            this.gboTree.Name = "gboTree";
            this.gboTree.Size = new System.Drawing.Size(313, 221);
            this.gboTree.TabIndex = 4;
            this.gboTree.TabStop = false;
            this.gboTree.Text = "Tree View:";
            // 
            // lblStats
            // 
            this.lblStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStats.AutoEllipsis = true;
            this.lblStats.Location = new System.Drawing.Point(6, 191);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(301, 23);
            this.lblStats.TabIndex = 1;
            this.lblStats.Text = "Size: 0   Max-Depth: 0   Min-Depth: 0";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeMainView
            // 
            this.treeMainView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMainView.HideSelection = false;
            this.treeMainView.ImageIndex = 0;
            this.treeMainView.ImageList = this.imageList1;
            this.treeMainView.Location = new System.Drawing.Point(6, 19);
            this.treeMainView.Name = "treeMainView";
            this.treeMainView.SelectedImageIndex = 0;
            this.treeMainView.Size = new System.Drawing.Size(301, 169);
            this.treeMainView.StateImageList = this.imageList1;
            this.treeMainView.TabIndex = 0;
            this.treeMainView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMainView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bullet_green16.png");
            this.imageList1.Images.SetKeyName(1, "bullet_yellow16.png");
            this.imageList1.Images.SetKeyName(2, "bullet_red16.png");
            // 
            // gboActions
            // 
            this.gboActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboActions.Controls.Add(this.btnGetMin);
            this.gboActions.Controls.Add(this.btnMax);
            this.gboActions.Controls.Add(this.btnClear);
            this.gboActions.Controls.Add(this.btnRetrieve);
            this.gboActions.Controls.Add(this.btnDelete);
            this.gboActions.Location = new System.Drawing.Point(3, 344);
            this.gboActions.Name = "gboActions";
            this.gboActions.Size = new System.Drawing.Size(594, 53);
            this.gboActions.TabIndex = 5;
            this.gboActions.TabStop = false;
            this.gboActions.Text = "User Actions:";
            // 
            // btnGetMin
            // 
            this.btnGetMin.Location = new System.Drawing.Point(430, 19);
            this.btnGetMin.Name = "btnGetMin";
            this.btnGetMin.Size = new System.Drawing.Size(100, 23);
            this.btnGetMin.TabIndex = 4;
            this.btnGetMin.Text = "Get Min";
            this.btnGetMin.UseVisualStyleBackColor = true;
            this.btnGetMin.Click += new System.EventHandler(this.btnGetMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.Location = new System.Drawing.Point(324, 19);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(100, 23);
            this.btnMax.TabIndex = 3;
            this.btnMax.Text = "Get Max";
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(218, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Enabled = false;
            this.btnRetrieve.Location = new System.Drawing.Point(112, 19);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(100, 23);
            this.btnRetrieve.TabIndex = 1;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(6, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 23);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BinTreeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gboActions);
            this.Controls.Add(this.gboTree);
            this.Controls.Add(this.gboList);
            this.Controls.Add(this.gboImpl);
            this.Controls.Add(this.gboInsert);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "BinTreeViewer";
            this.Size = new System.Drawing.Size(600, 400);
            this.gboInsert.ResumeLayout(false);
            this.gboInsert.PerformLayout();
            this.gboImpl.ResumeLayout(false);
            this.gboImpl.PerformLayout();
            this.gboList.ResumeLayout(false);
            this.gboTree.ResumeLayout(false);
            this.gboActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtInsert;
        private System.Windows.Forms.GroupBox gboInsert;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.GroupBox gboImpl;
        private System.Windows.Forms.RadioButton rdoSplay;
        private System.Windows.Forms.RadioButton rdoRedBlack;
        private System.Windows.Forms.RadioButton rdoAVL;
        private System.Windows.Forms.RadioButton rdoBasic;
        private System.Windows.Forms.GroupBox gboList;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.ComboBox cboSort;
        private System.Windows.Forms.GroupBox gboTree;
        private System.Windows.Forms.TreeView treeMainView;
        private System.Windows.Forms.GroupBox gboActions;
        private System.Windows.Forms.Button btnGetMin;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.ImageList imageList1;
    }
}
