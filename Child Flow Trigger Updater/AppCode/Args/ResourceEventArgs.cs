using System;


namespace ChildFlowTriggerUpdater.AppCode.Args
{
    public class ResourceEventArgs : EventArgs
    {
        public ResourceEventArgs(Flow resource)
        {
            Resource = resource;
        }

        public Flow Resource { get; }
    }
}
