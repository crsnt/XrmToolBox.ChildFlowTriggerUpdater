using ChildFlowTriggerUpdater.AppCode;
using ChildFlowTriggerUpdater.AppCode.Forms.Contents;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildFlowTriggerUpdater.Forms.Contents
{
    public partial class FlowJsonForm : BaseContentForm
    {
        public FlowJsonForm()
        {
        }

        public FlowJsonForm(MyPluginControl control, Flow resource) : base(control, resource, true)
        {
            InitializeComponent();

            resource.ContentReplaced += Resource_ContentReplaced;

            Text = resource.Name;

            this.KeyPreview = true; // Ensure the form receives key events
            this.KeyDown += new KeyEventHandler(BaseContentForm_KeyDown);
        }

        private void Resource_ContentReplaced(object sender, AppCode.Args.ResourceEventArgs e)
        {
            ShowJson(e.Resource.Content);
        }

        private void FlowJsonForm_Load(object sender, EventArgs e)
        {
            ShowJson(Resource.Content);
        }

        private void ShowJson(string jsonContent)
        {
            tvFlowJson.LoadJsonToTreeView(jsonContent);
            tvFlowJson.ExpandAll();
        }


        #region Search

        private void BaseContentForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                ShowSearchDialog();
            }
        }

        private void ShowSearchDialog()
        {
            using (Form searchForm = new Form())
            {
                searchForm.Text = "Search";
                searchForm.Size = new Size(300, 100);

                TextBox searchTextBox = new TextBox() { Dock = DockStyle.Fill };
                Button searchButton = new Button() { Text = "Search", Dock = DockStyle.Bottom };

                searchButton.Click += (s, e) =>
                {
                    string searchTerm = searchTextBox.Text;
                    SearchTreeView(tvFlowJson, searchTerm);
                    searchForm.Close();
                };

                // Handle the KeyDown event for the search text box
                searchTextBox.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        string searchTerm = searchTextBox.Text;
                        SearchTreeView(tvFlowJson, searchTerm);
                        e.Handled = true;
                        e.SuppressKeyPress = true; // Prevent the ding sound
                    }
                };

                searchForm.Controls.Add(searchTextBox);
                searchForm.Controls.Add(searchButton);
                searchForm.ShowDialog();
            }
        }

        private void SearchTreeView(TreeView treeView, string searchTerm)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                SearchNode(node, searchTerm);
            }
        }

        private void SearchNode(TreeNode node, string searchTerm)
        {
            if (node.Text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                tvFlowJson.SelectedNode = node;
                node.BackColor = Color.Yellow;
            }
            else
            {
                node.BackColor = tvFlowJson.BackColor;
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                SearchNode(childNode, searchTerm);
            }
        }

        #endregion
    }
}
