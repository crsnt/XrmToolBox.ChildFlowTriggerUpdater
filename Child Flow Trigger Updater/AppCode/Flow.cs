using ChildFlowTriggerUpdater.AppCode.Args;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace ChildFlowTriggerUpdater.AppCode
{
    public partial class Flow
    {
        #region Variables

        private Entity record;
        private Settings settings;
        private readonly DateTime loadedOn;

        #endregion Variables

        #region Constructors

        public Flow(Entity record, MyPluginControl parent, Settings settings) : this(settings)
        {
            this.record = record;

            loadedOn = DateTime.Now;
            Plugin = parent;
            ParentFlows = new List<Flow>();
        }

        public Flow(Settings settings)
        {
            this.settings = settings;
        }

        #endregion

        #region Properties

        [Browsable(false)]
        public MyPluginControl Plugin { get; internal set; }

        [Browsable(false)]
        public TreeNode Node { get; set; }

        [Category("Dates")]
        [DisplayName("Created By")]
        [Description("User who created this flow")]
        [ReadOnly(true)]
        public string CreatedBy => record.GetAttributeValue<EntityReference>("createdby")?.Id.ToString("B");

        [Category("Dates")]
        [DisplayName("Created On")]
        [Description("Date of creation")]
        [ReadOnly(true)]
        public string CreatedOn => record.Contains("createdon") ? record.FormattedValues["createdon"] : "";

        [Category("Attributes")]
        [DisplayName("Unique identifier")]
        [Description("Unique identifier of the flow")]
        [ReadOnly(true)]
        public Guid Id => record?.Id ?? Guid.Empty;

        [Category("Attributes")]
        [DisplayName("Workflow Unique identifier")]
        [Description("Workflow Unique identifier of the flow")]
        [ReadOnly(true)]
        public Guid IdUnique
        {
            get => record.GetAttributeValue<Guid>("workflowidunique");
        }

        [Category("Attributes")]
        [DisplayName("Name")]
        [Description("Name of the flow")]
        [ReadOnly(true)]
        public string Name
        {
            get => record?.GetAttributeValue<string>("name");
            set
            {
                record["name"] = value;
            }
        }

        [Category("Attributes")]
        [DisplayName("Description")]
        [Description("Description of the flow")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Description
        {
            get => record?.GetAttributeValue<string>("description");
            set
            {
                record["description"] = value;
            }
        }

        [Category("Attributes")]
        [DisplayName("Content")]
        [Description("Content of the flow")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Content
        {
            get => record?.GetAttributeValue<string>("clientdata");
            set
            {
                record["clientdata"] = value;
            }
        }

        [DisplayName("IsLoaded")]
        [Browsable(false)]
        [Description("Shows if flow content was loaded")]
        [ReadOnly(true)]
        public bool IsLoaded => !(string.IsNullOrWhiteSpace(Content));

        [Category("Type")]
        [DisplayName("Is Child")]
        [Description("Is Child Flow")]
        public bool IsChild { get; set; } = false;

        public IList<Flow> ParentFlows { get; set; }

        public bool NoChildActionRequired { get; set; }

        public bool NoParentActionRequired { get; set; }

        #endregion

        #region Events

        public event EventHandler<ResourceEventArgs> ContentReplaced;

        #endregion

        #region Methods

        public void GetLatestVersion()
        {
            Content = RetrieveFlow(Id, Plugin.Service).GetAttributeValue<string>("clientdata");
            ContentReplaced?.Invoke(this, new ResourceEventArgs(this));
        }

        public void LazyLoadFlow(IOrganizationService service)
        {
            Content = RetrieveFlow(Id, service).GetAttributeValue<string>("clientdata");
        }

        #endregion
    }
}
