namespace ChildFlowTriggerUpdater
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPluginControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsddCrmMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.TsmiLoadFromASpecificSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUpdateSelectedFlows = new System.Windows.Forms.ToolStripButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dpMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.cmsTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsTabsCloseExceptThis = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTabsCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu.SuspendLayout();
            this.cmsTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddCrmMenu,
            this.tssSeparator1,
            this.tsbUpdateSelectedFlows});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(559, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "mainToolStrip";
            // 
            // tsddCrmMenu
            // 
            this.tsddCrmMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiLoadFromASpecificSolution});
            this.tsddCrmMenu.Image = ((System.Drawing.Image)(resources.GetObject("tsddCrmMenu.Image")));
            this.tsddCrmMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddCrmMenu.Name = "tsddCrmMenu";
            this.tsddCrmMenu.Size = new System.Drawing.Size(62, 22);
            this.tsddCrmMenu.Text = "CRM";
            this.tsddCrmMenu.ToolTipText = "CRM";
            this.tsddCrmMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddCrmMenu_DropDownItemClicked);
            // 
            // TsmiLoadFromASpecificSolution
            // 
            this.TsmiLoadFromASpecificSolution.Name = "TsmiLoadFromASpecificSolution";
            this.TsmiLoadFromASpecificSolution.Size = new System.Drawing.Size(227, 22);
            this.TsmiLoadFromASpecificSolution.Text = "Load from a specific solution";
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbUpdateSelectedFlows
            // 
            this.tsbUpdateSelectedFlows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUpdateSelectedFlows.Name = "tsbUpdateSelectedFlows";
            this.tsbUpdateSelectedFlows.Size = new System.Drawing.Size(129, 22);
            this.tsbUpdateSelectedFlows.Text = "Update Selected Flows";
            this.tsbUpdateSelectedFlows.Click += new System.EventHandler(this.tsbUpdateSelectedFlows_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(559, 275);
            this.webBrowser1.TabIndex = 5;
            // 
            // dpMain
            // 
            this.dpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpMain.DockBackColor = System.Drawing.Color.White;
            this.dpMain.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dpMain.Location = new System.Drawing.Point(0, 25);
            this.dpMain.Name = "dpMain";
            this.dpMain.Size = new System.Drawing.Size(559, 275);
            this.dpMain.TabIndex = 6;
            // 
            // cmsTabs
            // 
            this.cmsTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsTabsCloseExceptThis,
            this.cmsTabsCloseAll});
            this.cmsTabs.Name = "cmsTabs";
            this.cmsTabs.Size = new System.Drawing.Size(199, 48);
            this.cmsTabs.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsTabs_ItemClicked);
            // 
            // cmsTabsCloseExceptThis
            // 
            this.cmsTabsCloseExceptThis.Name = "cmsTabsCloseExceptThis";
            this.cmsTabsCloseExceptThis.Size = new System.Drawing.Size(198, 22);
            this.cmsTabsCloseExceptThis.Text = "Close all except this tab";
            // 
            // cmsTabsCloseAll
            // 
            this.cmsTabsCloseAll.Name = "cmsTabsCloseAll";
            this.cmsTabsCloseAll.Size = new System.Drawing.Size(198, 22);
            this.cmsTabsCloseAll.Text = "Close all tabs";
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dpMain);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MyPluginControl";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(559, 300);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.cmsTabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbUpdateSelectedFlows;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsddCrmMenu;
        private System.Windows.Forms.ToolStripMenuItem TsmiLoadFromASpecificSolution;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dpMain;
        private System.Windows.Forms.ContextMenuStrip cmsTabs;
        private System.Windows.Forms.ToolStripMenuItem cmsTabsCloseExceptThis;
        private System.Windows.Forms.ToolStripMenuItem cmsTabsCloseAll;
    }
}
