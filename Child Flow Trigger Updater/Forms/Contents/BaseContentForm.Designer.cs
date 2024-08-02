namespace ChildFlowTriggerUpdater.AppCode.Forms.Contents
{
    partial class BaseContentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseContentForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsSeparatorEdit = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGetLatestVersion = new System.Windows.Forms.ToolStripButton();
            this.tssComments = new System.Windows.Forms.ToolStripSeparator();
            this.tslFlow = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSeparatorEdit,
            this.tsbGetLatestVersion,
            this.tssComments,
            this.tslFlow});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(745, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsSeparatorEdit
            // 
            this.tsSeparatorEdit.Name = "tsSeparatorEdit";
            this.tsSeparatorEdit.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbGetLatestVersion
            // 
            this.tsbGetLatestVersion.Image = ((System.Drawing.Image)(resources.GetObject("tsbGetLatestVersion.Image")));
            this.tsbGetLatestVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGetLatestVersion.Name = "tsbGetLatestVersion";
            this.tsbGetLatestVersion.Size = new System.Drawing.Size(79, 22);
            this.tsbGetLatestVersion.Text = "Get Latest";
            this.tsbGetLatestVersion.Click += new System.EventHandler(this.tsbGetLatestVersion_Click);
            // 
            // tssComments
            // 
            this.tssComments.Name = "tssComments";
            this.tssComments.Size = new System.Drawing.Size(6, 25);
            // 
            // tslFlow
            // 
            this.tslFlow.Name = "tslFlow";
            this.tslFlow.Size = new System.Drawing.Size(71, 22);
            this.tslFlow.Text = "[flow name]";
            // 
            // BaseContentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 159);
            this.Controls.Add(this.toolStrip);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BaseContentForm";
            this.Text = "BaseContentForm";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator tsSeparatorEdit;
        private System.Windows.Forms.ToolStripButton tsbGetLatestVersion;
        private System.Windows.Forms.ToolStripSeparator tssComments;
        private System.Windows.Forms.ToolStripLabel tslFlow;
    }
}