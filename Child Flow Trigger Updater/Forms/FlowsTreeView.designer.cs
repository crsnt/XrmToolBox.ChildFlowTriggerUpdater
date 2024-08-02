
namespace ChildFlowTriggerUpdater.Forms
{
    partial class FlowsTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowsTreeView));
            this.tsTvMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCheckAll = new System.Windows.Forms.ToolStripButton();
            this.tsbClearAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExpandAll = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkDisplayExpanded = new System.Windows.Forms.CheckBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.tv = new System.Windows.Forms.TreeView();
            this.ilFlowTypes = new System.Windows.Forms.ImageList(this.components);
            this.pnlSolution = new System.Windows.Forms.Panel();
            this.lblSolution = new System.Windows.Forms.Label();
            this.tsTvMain.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlSolution.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsTvMain
            // 
            this.tsTvMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTvMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tsbCheckAll,
            this.tsbClearAll,
            this.toolStripSeparator2,
            this.tsbExpandAll,
            this.tsbCollapseAll});
            this.tsTvMain.Location = new System.Drawing.Point(0, 0);
            this.tsTvMain.Name = "tsTvMain";
            this.tsTvMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsTvMain.Size = new System.Drawing.Size(319, 25);
            this.tsTvMain.TabIndex = 8;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCheckAll
            // 
            this.tsbCheckAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCheckAll.Image = global::ChildFlowTriggerUpdater.Properties.Resources.check_box;
            this.tsbCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckAll.Name = "tsbCheckAll";
            this.tsbCheckAll.Size = new System.Drawing.Size(23, 22);
            this.tsbCheckAll.Text = "Check all";
            this.tsbCheckAll.ToolTipText = "Check all flows";
            this.tsbCheckAll.Click += new System.EventHandler(this.tsbCheckAll_Click);
            // 
            // tsbClearAll
            // 
            this.tsbClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClearAll.Image = global::ChildFlowTriggerUpdater.Properties.Resources.check_box_uncheck;
            this.tsbClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearAll.Name = "tsbClearAll";
            this.tsbClearAll.Size = new System.Drawing.Size(23, 22);
            this.tsbClearAll.Text = "Clear all";
            this.tsbClearAll.ToolTipText = "Uncheck all flows";
            this.tsbClearAll.Click += new System.EventHandler(this.tsbClearAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExpandAll
            // 
            this.tsbExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbExpandAll.Image")));
            this.tsbExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExpandAll.Name = "tsbExpandAll";
            this.tsbExpandAll.Size = new System.Drawing.Size(65, 22);
            this.tsbExpandAll.Text = "Expand all";
            this.tsbExpandAll.Click += new System.EventHandler(this.tsbExpandAll_Click);
            // 
            // tsbCollapseAll
            // 
            this.tsbCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbCollapseAll.Image")));
            this.tsbCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollapseAll.Name = "tsbCollapseAll";
            this.tsbCollapseAll.Size = new System.Drawing.Size(71, 22);
            this.tsbCollapseAll.Text = "Collapse all";
            this.tsbCollapseAll.Click += new System.EventHandler(this.tsbCollapseAll_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 629);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(319, 50);
            this.pnlBottom.TabIndex = 10;
            this.pnlBottom.Visible = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.chkDisplayExpanded);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSearch.Location = new System.Drawing.Point(0, 607);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(319, 22);
            this.pnlSearch.TabIndex = 86;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(41, 0);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(256, 20);
            this.txtSearch.TabIndex = 92;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // chkDisplayExpanded
            // 
            this.chkDisplayExpanded.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDisplayExpanded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkDisplayExpanded.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkDisplayExpanded.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.chkDisplayExpanded.FlatAppearance.CheckedBackColor = System.Drawing.Color.PowderBlue;
            this.chkDisplayExpanded.Image = ((System.Drawing.Image)(resources.GetObject("chkDisplayExpanded.Image")));
            this.chkDisplayExpanded.Location = new System.Drawing.Point(297, 0);
            this.chkDisplayExpanded.Name = "chkDisplayExpanded";
            this.chkDisplayExpanded.Size = new System.Drawing.Size(22, 22);
            this.chkDisplayExpanded.TabIndex = 91;
            this.chkDisplayExpanded.UseVisualStyleBackColor = true;
            this.chkDisplayExpanded.CheckedChanged += new System.EventHandler(this.chkDisplayExpanded_CheckedChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Location = new System.Drawing.Point(0, 0);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblSearch.Size = new System.Drawing.Size(41, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // tv
            // 
            this.tv.AllowDrop = true;
            this.tv.CheckBoxes = true;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.HideSelection = false;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.ilFlowTypes;
            this.tv.Location = new System.Drawing.Point(0, 45);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(319, 562);
            this.tv.TabIndex = 87;
            this.tv.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCheck);
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // ilFlowTypes
            // 
            this.ilFlowTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilFlowTypes.ImageStream")));
            this.ilFlowTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.ilFlowTypes.Images.SetKeyName(0, "baby.png");
            this.ilFlowTypes.Images.SetKeyName(1, "parents.png");
            // 
            // pnlSolution
            // 
            this.pnlSolution.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSolution.Controls.Add(this.lblSolution);
            this.pnlSolution.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSolution.Location = new System.Drawing.Point(0, 25);
            this.pnlSolution.Name = "pnlSolution";
            this.pnlSolution.Size = new System.Drawing.Size(319, 20);
            this.pnlSolution.TabIndex = 88;
            this.pnlSolution.Visible = false;
            // 
            // lblSolution
            // 
            this.lblSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSolution.Location = new System.Drawing.Point(0, 0);
            this.lblSolution.Name = "lblSolution";
            this.lblSolution.Size = new System.Drawing.Size(319, 20);
            this.lblSolution.TabIndex = 0;
            this.lblSolution.Text = "[solution loaded]";
            this.lblSolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FlowsTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(319, 679);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tv);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlSolution);
            this.Controls.Add(this.tsTvMain);
            this.Name = "FlowsTreeView";
            this.Text = "Flows Explorer";
            this.Load += new System.EventHandler(this.FlowsTreeView_Load);
            this.Enter += new System.EventHandler(this.FlowsTreeView_Enter);
            this.tsTvMain.ResumeLayout(false);
            this.tsTvMain.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlSolution.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsTvMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCheckAll;
        private System.Windows.Forms.ToolStripButton tsbClearAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbExpandAll;
        private System.Windows.Forms.ToolStripButton tsbCollapseAll;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkDisplayExpanded;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ImageList ilFlowTypes;
        private System.Windows.Forms.Panel pnlSolution;
        private System.Windows.Forms.Label lblSolution;
    }
}