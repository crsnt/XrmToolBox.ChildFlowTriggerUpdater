using ChildFlowTriggerUpdater.AppCode;
using ChildFlowTriggerUpdater.AppCode.Args;
using ChildFlowTriggerUpdater.CustomControls;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ChildFlowTriggerUpdater.Forms
{
    public partial class FlowsTreeView : DockContent
    {
        private readonly MyPluginControl mainControl;
        private Entity solution;

        public FlowsTreeView()
        {
            InitializeComponent();

            ToolTip tip = new ToolTip();
            tip.SetToolTip(chkDisplayExpanded, "Display results as expanded");
        }

        public FlowsTreeView(MyPluginControl mainControl) : this()
        {
            this.mainControl = mainControl;
        }

        public event EventHandler<NodeSelectedEventArgs> ContextMenuRequested;

        public event EventHandler<ResourceEventArgs> ResourceDisplayRequested;

        public event EventHandler ShowPendingUpdatesRequested;

        public List<Flow> CheckedFlows
        {
            get
            {
                return GetCheckedNodes(tv.Nodes);
            }
        }

        public int OrganizationMajorVersion { get; set; }

        public IOrganizationService Service { get; set; }

        public Settings Settings { get; set; }

        private List<Flow> GetCheckedNodes(TreeNodeCollection nodes)
        {
            var checkedFlows = new List<Flow>();

            foreach (TreeNode aNode in nodes.Cast<TreeNode>().Where(n => n.Checked))
            {
                if (aNode is ChildFlowNode childFlowNode)
                {
                    var nodeFlow = childFlowNode.Resource;

                    if (aNode.Nodes.Count > 0)
                    {
                        nodeFlow.ParentFlows = aNode.Nodes.Cast<TreeNode>()
                            .Where(n => n.Checked && n is FlowNode)
                            .Select(n => ((FlowNode)n).Resource)
                            .ToList();
                    }

                    checkedFlows.Add(nodeFlow);
                }
            }

            return checkedFlows;
        }

        private void FlowsTreeView_Enter(object sender, EventArgs e)
        {
            pnlSearch.Dock = DockStyle.Top;
            Invalidate();
        }

        private void FlowsTreeView_Load(object sender, EventArgs e)
        {
        }

        #region Treeview Display

        public FlowNode AddSingleNode(Flow resource, bool childAsParent = false)
        {
            FlowNode node;

            if (resource.IsChild)
            {
                node = new ChildFlowNode(resource);

                if (resource.NoChildActionRequired ||
                    (childAsParent && resource.NoParentActionRequired))
                {
                    node.ForeColor = Color.Gray;
                }
            }
            else
            {
                node = new ParentFlowNode(resource);

                if (resource.NoParentActionRequired)
                {
                    node.ForeColor = Color.Gray;
                }
            }

            resource.Node = node;

            return node;
        }

        public void DisplayNodes(IEnumerable<Flow> resources, Entity theSolution = null, bool expanded = false)
        {
            Invoke(new Action(() =>
            {
                //pnlTop.Visible = false;

                solution = theSolution;
                if (solution != null)
                {
                    lblSolution.Text =
                        $@"{solution.GetAttributeValue<string>("friendlyname")} ({solution.GetAttributeValue<string>("version")})";
                    pnlSolution.Visible = true;
                }
            }));

            var resourcesToDisplay = mainControl.FlowsCache.Where(w => txtSearch.Text.Length == 0
                                                                || w.Name.ToString().ToLower()
                                                                    .Contains(txtSearch.Text.ToLower())).ToList();

            if (!resourcesToDisplay.Any() && txtSearch.Text.Length > 0)
            {
                txtSearch.BackColor = Color.LightCoral;
                return;
            }

            var rootNodes = new List<TreeNode>();

            var invalidFilesList = new List<string>();

            var orderedResources = resourcesToDisplay.OrderBy(r => r.Name.ToLower());
            foreach (var resource in orderedResources)
            {
                var node = AddSingleNode(resource);
                if (node != null)
                {
                    rootNodes.Add(node);

                    foreach (var parent in resource.ParentFlows)
                    {
                        node.Nodes.Add(AddSingleNode(parent, parent.IsChild));
                    }
                }
            }

            tv.Nodes.Clear();
            tv.Nodes.AddRange(rootNodes.ToArray());
            tv.Sort();

            if (expanded)
            {
                tv.ExpandAll();
            }
        }

        #endregion Treeview Display

        #region Search bar

        private Thread searchThread;

        private void chkDisplayExpanded_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisplayExpanded.Checked)
            {
                tsbExpandAll_Click(tsbExpandAll, new EventArgs());
            }
            else
            {
                tsbCollapseAll_Click(tsbCollapseAll, new EventArgs());
            }
        }

        private void chkSearchInContent_CheckedChanged(object sender, EventArgs e)
        {
            txtSearch.BackColor = SystemColors.Window;
            searchThread?.Abort();
            searchThread = new Thread(DisplayWrs);
            searchThread.Start();
        }

        private void DisplayWrs()
        {
            tv.Invoke(new Action(() =>
            {
                DisplayNodes(mainControl.FlowsCache, solution, chkDisplayExpanded.Checked);
            }));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txtSearch.BackColor = SystemColors.Window;
            searchThread?.Abort();
            searchThread = new Thread(DisplayWrs);
            searchThread.Start();
        }

        #endregion Search bar

        #region Menu events

        private void tsbCheckAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tv.Nodes)
                node.Checked = true;
        }

        private void tsbClearAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tv.Nodes)
                node.Checked = false;
        }

        private void tsbCollapseAll_Click(object sender, EventArgs e)
        {
            tv.CollapseAll();
        }

        private void tsbExpandAll_Click(object sender, EventArgs e)
        {
            tv.ExpandAll();
        }

        #endregion Menu events

        #region Treeview events

        private void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            (e.Node as FlowNode)?.SetOtherNodesCheckState();
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tv.SelectedNode = e.Node;

            ResourceDisplayRequested?.Invoke(this, new ResourceEventArgs((e.Node as FlowNode)?.Resource));
        }

        #endregion Treeview events
    }
}