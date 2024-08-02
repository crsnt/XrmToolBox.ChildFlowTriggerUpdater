using ChildFlowTriggerUpdater.AppCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChildFlowTriggerUpdater.CustomControls
{
    public class ChildFlowNode : FlowNode
    {
        private const int ImageIndex = 0;
        public ChildFlowNode(Flow resource) : base(resource, ImageIndex)
        {
        }
    }
}
