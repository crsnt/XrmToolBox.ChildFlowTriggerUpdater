using ChildFlowTriggerUpdater.AppCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChildFlowTriggerUpdater.CustomControls
{
    public class ParentFlowNode : FlowNode
    {
        private const int ImageIndex = 1;

        public ParentFlowNode(Flow resource) : base(resource, ImageIndex)
        {
        }
    }
}
