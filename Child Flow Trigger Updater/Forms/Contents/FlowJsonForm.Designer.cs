namespace ChildFlowTriggerUpdater.Forms.Contents
{
    partial class FlowJsonForm
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
            this.tvFlowJson = new System.Windows.Forms.TreeView();
            this.pnlFlowJsonForm = new System.Windows.Forms.Panel();
            this.pnlFlowJsonForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvFlowJson
            // 
            this.tvFlowJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFlowJson.Location = new System.Drawing.Point(0, 25);
            this.tvFlowJson.Margin = new System.Windows.Forms.Padding(3, 25, 3, 3);
            this.tvFlowJson.Name = "tvFlowJson";
            this.tvFlowJson.Size = new System.Drawing.Size(536, 260);
            this.tvFlowJson.TabIndex = 0;
            // 
            // pnlFlowJsonForm
            // 
            this.pnlFlowJsonForm.Controls.Add(this.tvFlowJson);
            this.pnlFlowJsonForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFlowJsonForm.Location = new System.Drawing.Point(0, 0);
            this.pnlFlowJsonForm.Name = "pnlFlowJsonForm";
            this.pnlFlowJsonForm.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.pnlFlowJsonForm.Size = new System.Drawing.Size(536, 285);
            this.pnlFlowJsonForm.TabIndex = 1;
            // 
            // FlowJsonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 285);
            this.Controls.Add(this.pnlFlowJsonForm);
            this.Name = "FlowJsonForm";
            this.Text = "FlowJsonForm";
            this.Load += new System.EventHandler(this.FlowJsonForm_Load);
            this.pnlFlowJsonForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvFlowJson;
        private System.Windows.Forms.Panel pnlFlowJsonForm;
    }
}