using ChildFlowTriggerUpdater.AppCode;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ChildFlowTriggerUpdater.Forms
{
    public partial class SettingsDialog : DockContent
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        public Settings Settings
        {
            set
            {
                if (propertyGrid1.SelectedObject != value)
                {
                    propertyGrid1.SelectedObject = value;
                }
            }
        }

        public event EventHandler OnSettingsChanged;

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            raiseOnSettingsChanged();
        }

        private void raiseOnSettingsChanged()
        {
            OnSettingsChanged?.Invoke(this, new EventArgs());
        }

        private void onSettingChangedCustom(object s, EventArgs e)
        {
            raiseOnSettingsChanged();
        }
    }
}