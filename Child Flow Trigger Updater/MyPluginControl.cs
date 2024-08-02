using ChildFlowTriggerUpdater.AppCode;
using ChildFlowTriggerUpdater.AppCode.Args;
using ChildFlowTriggerUpdater.AppCode.Forms.Contents;
using ChildFlowTriggerUpdater.Forms;
using ChildFlowTriggerUpdater.Forms.Contents;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace ChildFlowTriggerUpdater
{
    public partial class MyPluginControl : PluginControlBase, IGitHubPlugin, IPayPalPlugin
    {
        private FlowsTreeView tv;
        private SettingsDialog sd;
        private Settings settings;
        private Entity solution;

        #region IGitHubPlugin

        public virtual string RepositoryName => "XrmToolBox.ChildFlowTriggerUpdater";
        public virtual string UserName => "crsnt";

        #endregion IGitHubPlugin

        public MyPluginControl()
        {
            InitializeComponent();

            SetTheme();

            tv = new FlowsTreeView(this);
            tv.ResourceDisplayRequested += Tv_ResourceDisplayRequested;

            tv.Show(dpMain, DockState.DockLeft);

            sd = new SettingsDialog();
            sd.OnSettingsChanged += (sender, e) =>
            {
                settings.Save(ConnectionDetail?.ConnectionId.ToString());

                // Reload flows
                LoadChildFlows(solution);
            };
        }

        public ObservableCollection<Flow> FlowsCache { get; set; } = new ObservableCollection<Flow>();

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            settings.SettingsDockState = sd.DockState;

            settings.Save(ConnectionDetail?.ConnectionId.ToString());

            base.ClosingPlugin(info);
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
        }

        private void tsbUpdateSelectedFlows_Click(object sender, EventArgs e)
        {
            // Show a confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure you want to proceed?\nMake sure to back up the existing solution.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the result of the dialog
            if (result == DialogResult.Yes)
            {
                ExecuteMethod(UpdateSelectedFlows);
            }
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            try
            {
                if (!SettingsManager.Instance.TryLoad(GetType(), out settings, detail.ConnectionId.ToString()))
                {
                    settings = new Settings();
                }
            }
            catch
            {
                settings = new Settings();
            }

            sd.Show(dpMain, settings.SettingsDockState);
            sd.Settings = settings;

            base.UpdateConnection(newService, detail, actionName, parameter);
        }

        private void asToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tsddCrmMenu_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == TsmiLoadFromASpecificSolution)
            {
                ExecuteMethod(LoadChildFlowsFromSolution);
            }
        }

        private void Tv_ResourceDisplayRequested(object sender, ResourceEventArgs e)
        {
            DisplayContent(e.Resource);
        }

        private void DisplayContent(Flow resource)
        {
            if (resource == null) return;

            var existingContent = dpMain.Contents.OfType<BaseContentForm>().FirstOrDefault(c => c.Resource.Id == resource.Id);
            if (existingContent != null)
            {
                existingContent.Show(dpMain, existingContent.DockState);
                return;
            }

            if (!resource.IsLoaded)
            {
                LazyLoadFlow(resource);
            }
            else
            {
                DisplayContentForm(resource);
            }
        }

        private void LazyLoadFlow(Flow resource)
        {
            WorkAsync(
                new WorkAsyncInfo("Loading web resource...", e =>
                {
                    resource.LazyLoadFlow(Service);
                })
                {
                    PostWorkCallBack = e =>
                    {
                        DisplayContentForm(resource);
                    }
                });
        }

        private void DisplayContentForm(Flow resource)
        {
            BaseContentForm content = null;

            content = new FlowJsonForm(this, resource);

            if (content != null)
            {
                content.Settings = settings;
                content.TabPageContextMenuStrip = cmsTabs;
                content.Show(dpMain, DockState.Document);
            }
        }

        private void cmsTabs_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == cmsTabsCloseExceptThis)
            {
                var currentContent = dpMain.ActiveContent as BaseContentForm;
                var list = dpMain.Contents.OfType<BaseContentForm>().Where(bcf => bcf != currentContent).ToList();
                foreach (var content in list)
                {
                    if (content != currentContent)
                    {
                        content.Close();
                        content.Dispose();
                    }
                }
            }
            else if (e.ClickedItem == cmsTabsCloseAll)
            {
                CloseAllTabs();
            }
        }

        private void CloseAllTabs()
        {
            var list = dpMain.Contents.OfType<BaseContentForm>().ToList();
            foreach (var content in list)
            {
                content.Close();
                content.Dispose();
            }
        }

        private void SetTheme()
        {
            if (XrmToolBox.Options.Instance.Theme != null)
            {
                switch (XrmToolBox.Options.Instance.Theme)
                {
                    case "Blue theme":
                        {
                            var theme = new VS2015BlueTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case "Light theme":
                        {
                            var theme = new VS2015LightTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case "Dark theme":
                        {
                            var theme = new VS2015DarkTheme();
                            dpMain.Theme = theme;
                        }
                        break;
                }
            }
        }

        #region CRM

        public void LoadChildFlowsFromSolution()
        {
            using (var sPicker = new SolutionPicker(Service) { StartPosition = FormStartPosition.CenterParent })
            {
                if (sPicker?.ShowDialog(ParentForm) == DialogResult.OK)
                {
                    solution = sPicker.SelectedSolution;
                }
                else
                {
                    return;
                }
            }

            LoadChildFlows(solution);
            CloseAllTabs();
        }

        private void LoadChildFlows(Entity solution)
        {
            tv.Enabled = false;

            //CloseOpenedContents();

            WorkAsync(new WorkAsyncInfo("Loading child flows...", e =>
            {
                var request = new RetrieveProvisionedLanguagesRequest();
                var response = (RetrieveProvisionedLanguagesResponse)Service.Execute(request);

                var items = Flow.RetrieveChildFlows(this, Service, solution?.Id ?? Guid.Empty, settings);
                e.Result = items;

                FlowsCache.Clear();

                foreach (var item in items)
                {
                    FlowsCache.Add(item);
                }
            })
            {
                AsyncArgument = solution,
                PostWorkCallBack = e =>
                {
                    tv.Enabled = true;
                    tv.DisplayNodes((IEnumerable<Flow>)e.Result, solution);
                }
            });
        }

        private void UpdateSelectedFlows()
        {
            var selectedChildFlows = tv.CheckedFlows;

            WorkAsync(new WorkAsyncInfo
            {
                AsyncArgument = selectedChildFlows,
                Message = "Updating Selected Flows..",
                Work = (worker, args) =>
                {
                    bool hasError = false;
                    var itemsToProcess = (List<Flow>)args.Argument;

                    foreach(var childFlow in selectedChildFlows)
                    {
                        try
                        {
                            var updateStatus = Flow.UpdateSelectedFlows(this, Service, childFlow, settings);
                            args.Result = updateStatus;
                        }
                        catch (FaultException<OrganizationServiceFault> error)
                        {
                            if (itemsToProcess.Count == 1)
                            {
                                args.Result = $"Child flow {childFlow.Name} was not updated because the flow or its parent/s has changed since it has been loaded";
                                continue;
                            }

                            if (error.Detail.ErrorCode == -2147088254)
                            {
                                LogError($"Child flow {childFlow.Name} was not updated because the flow or its parent/s has changed since it has been loaded");

                                hasError = true;
                                continue;
                            }

                            throw;
                        }
                    }

                    if (hasError)
                    {
                        throw new Exception("At least one record has not been updated. Please review the logs");
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        ShowErrorDialog(args.Error);
                        return;
                    }
                    var result = args.Result as string;
                    if (result != null)
                    {
                        MessageBox.Show(result);

                        // Reload flows
                        LoadChildFlows(solution);
                    }
                }
            });
        }

        #endregion

        #region IPayPalPlugin implementation

        public string DonationDescription => "Support Development for the Child Flow Trigger Updater!";

        public string EmailAccount => "blast_fenrir@hotmail.com";

        #endregion IPayPalPlugin implementation
    }
}