using ChildFlowTriggerUpdater.AppCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChildFlowTriggerUpdater.CustomControls
{
    public class FlowNode : TreeNode
    {
        public FlowNode(Flow resource, int imageIndex)
        {
            Resource = resource;
            Text = resource.Name;
            Name = $"{resource.Name}-{resource.Id}";
            ImageIndex = imageIndex;
            SelectedImageIndex = imageIndex;
        }

        public Flow Resource { get; set; }

        public void SetOtherNodesCheckState()
        {
            foreach (TreeNode node in Nodes)
            {
                node.Checked = Checked;
            }
        }
    }
}
