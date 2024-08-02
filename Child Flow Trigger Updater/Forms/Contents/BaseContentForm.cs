using System;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace ChildFlowTriggerUpdater.AppCode.Forms.Contents
{
    public abstract partial class BaseContentForm : DockContent
    {
        protected Size SavedSize;
        private readonly MyPluginControl mainControl;

        public BaseContentForm()
        {
        }

        public BaseContentForm(MyPluginControl mainControl, bool isCode = false, bool isImage = false)
        {
            InitializeComponent();

            this.mainControl = mainControl;
            tssComments.Visible = isCode;

        }

        public BaseContentForm(MyPluginControl mainControl, Flow resource, bool isCode = false, bool isImage = false) : this(mainControl, isCode, isImage)
        {
            Resource = resource;

            tslFlow.Text = $"{resource.Name} - {resource.Id}";
        }

        public Flow Resource { get; }
        public Settings Settings { get; set; }

        private void tsbGetLatestVersion_Click(object sender, EventArgs e)
        {
            Resource.GetLatestVersion();
        }
    }
}