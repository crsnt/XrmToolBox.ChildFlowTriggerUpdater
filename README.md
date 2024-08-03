# XrmToolBox.ChildFlowTriggerUpdater

This tool will update the child flow triggers to include the parent URL and will update all parent flows to include their flow run URLs.
![Screens](https://github.com/user-attachments/assets/54b79874-f7b5-48f9-9903-7a311d9c60fa)

# Background

When you have a child flow that is called by more than one parent flow, it is difficult to troubleshoot when things go wrong. The most common way to address this is to pass the parent flow run URL to the child flow as one of the trigger inputs. 

Developers need to always remember to add this. In big projects, this best practice can be easily missed, and it can be time-consuming to address this one by one.

# Purpose

- This tool will help you load all child flows with their respective parents from your solution.
- Provide the ability to add the trigger to the child flow and pass the parent flow run URLs **in bulk!**
- Support updating child flow that is also a parent of other flows
- Support updating parent flow that calls same child flows multiple times (e.g. different branching logic)
- Allow you to review the JSON content of the flow.

# Steps

1. Select your solution.
2. Confirm the **Setting -> Child Trigger Input Name** in the Settings dialog (right side of the screen) for the trigger input name.
   <br/>Note: If you have existing trigger input with different name, it will add new one. Existing trigger input with the same name will be skipped.
4. Check the child/parent flows that you want to update.
   <br/>Note: Flows that don't require updates will be in gray colour and skipped. If there is a parent flow that needs update, make sure that the child is ticked regardless.
5. Click 'Update Selected Flows'.
   <br/>Note: Each child flow and their direct parents updates are executed inside a transaction to ensure consistency.

# Credits

- Existing MscrmTools GitHub code for a great starting point.
- [JsonTreeView](https\://github.com/huseyint/JsonTreeView) loader.
- Icons created by [Freepik](https\://www.freepik.com/icons) for an amazing icon library.
