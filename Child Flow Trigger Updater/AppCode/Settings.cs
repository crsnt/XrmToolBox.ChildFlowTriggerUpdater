using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;

namespace ChildFlowTriggerUpdater.AppCode
{
    public class Settings
    {
        [Category("General")]
        [DisplayName("Child Trigger Input Name")]
        [Description("Child trigger input name that stores the parent workflow run URl")]
        public string ChildTriggerInputName { get; set; } = "ParentFlowRunUrl";

        [Browsable(false)] public DockState SettingsDockState { get; set; } = DockState.DockRightAutoHide;

        public void Save(string name = null)
        {
            SettingsManager.Instance.Save(GetType(), this, name);
        }
    }
}
