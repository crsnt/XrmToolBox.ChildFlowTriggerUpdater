﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using ChildFlowTriggerUpdater.AppCode;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows.Forms;

namespace ChildFlowTriggerUpdater.Forms
{
    public partial class SolutionPicker : Form
    {
        private readonly IOrganizationService innerService;

        public SolutionPicker(IOrganizationService service)
        {
            InitializeComponent();

            innerService = service;
        }

        public bool FilterByLcid { get; private set; }

        public Entity SelectedSolution { get; set; }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchSolutionByFriendlyName();
        }

        private void btnSolutionPickerCancel_Click(object sender, EventArgs e)
        {
            SelectedSolution = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSolutionPickerValidate_Click(object sender, EventArgs e)
        {
            if (lstSolutions.SelectedItems.Count > 0)
            {
                SelectedSolution = (Entity)lstSolutions.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, @"Please select a solution!", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lstSolutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var list = (ListView)sender;
            list.Sorting = list.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            list.ListViewItemSorter = new ListViewItemComparer(e.Column, list.Sorting);
        }

        private void lstSolutions_DoubleClick(object sender, EventArgs e)
        {
            btnSolutionPickerValidate_Click(null, null);
        }

        private EntityCollection RetrieveSolutions(string friendlyName = null)
        {
            try
            {
                QueryExpression qe = new QueryExpression("solution");
                qe.Distinct = true;
                qe.ColumnSet = Solution.Columns;
                qe.Criteria = new FilterExpression();
                qe.Criteria.AddCondition(new ConditionExpression("isvisible", ConditionOperator.Equal, true));
                qe.Criteria.AddCondition(new ConditionExpression("uniquename", ConditionOperator.NotEqual, "Default"));

                if (!string.IsNullOrEmpty(friendlyName))
                {
                    qe.Criteria.AddCondition(new ConditionExpression("friendlyname", ConditionOperator.Like, $"%{friendlyName}%"));
                }

                if (!chkDisplayManaged.Checked)
                {
                    qe.Criteria.AddCondition(new ConditionExpression("ismanaged", ConditionOperator.Equal, false));
                }

                return innerService.RetrieveMultiple(qe);
            }
            catch (Exception error)
            {
                if (error.InnerException is FaultException)
                {
                    throw new Exception("Error while retrieving solutions: " + error.InnerException.Message);
                }

                throw new Exception("Error while retrieving solutions: " + error.Message);
            }
        }

        private void SearchSolutionByFriendlyName()
        {
            lstSolutions.Items.Clear();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_SearchByFriendlyName;
            worker.RunWorkerCompleted += worker_SearchCompleted;
            worker.RunWorkerAsync();
        }

        private void SolutionPicker_Load(object sender, EventArgs e)
        {
            lstSolutions.Items.Clear();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void txbSolutionNameFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                SearchSolutionByFriendlyName();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = RetrieveSolutions();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Entity solution in ((EntityCollection)e.Result).Entities)
            {
                ListViewItem item = new ListViewItem(solution["friendlyname"].ToString());
                item.SubItems.Add(solution["version"].ToString());
                item.SubItems.Add(((EntityReference)solution["publisherid"]).Name);
                item.Tag = solution;

                lstSolutions.Items.Add(item);
            }

            lstSolutions.Enabled = true;
            btnSolutionPickerValidate.Enabled = true;
        }

        private void worker_SearchByFriendlyName(object sender, DoWorkEventArgs e)
        {
            e.Result = RetrieveSolutions(txbSolutionNameFilter.Text);
        }

        private void worker_SearchCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Entity solution in ((EntityCollection)e.Result).Entities)
            {
                ListViewItem item = new ListViewItem(solution.GetAttributeValue<string>("friendlyname"));
                item.SubItems.Add(solution.GetAttributeValue<string>("version"));
                item.SubItems.Add(solution.GetAttributeValue<EntityReference>("publisherid").Name);
                item.Tag = solution;

                lstSolutions.Items.Add(item);
            }

            lstSolutions.Enabled = true;
            btnSolutionPickerValidate.Enabled = true;
        }
    }
}