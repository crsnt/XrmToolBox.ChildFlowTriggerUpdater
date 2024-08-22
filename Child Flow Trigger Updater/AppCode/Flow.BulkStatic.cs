using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChildFlowTriggerUpdater.AppCode
{
    public partial class Flow
    {
        public static readonly ColumnSet Columns = new ColumnSet(
            "workflowid", "description", "category", "createdon", "createdby", "name", "workflowidunique", "modifiedby",
            "statecode", "clientdata", "modifiedon", "createdby");

        public static Entity RetrieveFlow(Guid flowId, IOrganizationService service)
        {
            try
            {
                Entity result = service.Retrieve("workflow", flowId, Flow.Columns);

                return result;
            }
            catch (Exception error)
            {
                throw new Exception($"An error occured while retrieving a flow with id {flowId.ToString()}: {error.Message}");
            }
        }

        public static IEnumerable<Flow> RetrieveChildFlows(MyPluginControl parent, IOrganizationService service, Guid solutionId, Settings settings)
        {
            try
            {
                var qba = new QueryByAttribute("solutioncomponent") { ColumnSet = new ColumnSet("objectid") };
                qba.Attributes.AddRange("solutionid", "componenttype");
                qba.Values.AddRange(solutionId, 29);

                var components = service.RetrieveMultiple(qba).Entities;

                var list =
                    components.Select(component => component.GetAttributeValue<Guid>("objectid").ToString("B"))
                        .ToList();

                if (list.Count > 0)
                {
                    var qe = new QueryExpression("workflow")
                    {
                        ColumnSet = Flow.Columns,
                        Criteria = new FilterExpression
                        {
                            Filters =
                            {
                                new FilterExpression
                                {
                                    FilterOperator = LogicalOperator.And,
                                    Conditions =
                                    {
                                        new ConditionExpression("workflowid", ConditionOperator.In, list.ToArray()),
                                    }
                                },
                                new FilterExpression
                                {
                                    FilterOperator = LogicalOperator.Or,
                                     Conditions =
                                    {
                                        new ConditionExpression("category", ConditionOperator.Equal, 5),
                                        new ConditionExpression("ismanaged", ConditionOperator.Equal, false),
                                        new ConditionExpression("iscustomizable", ConditionOperator.Equal, true),
                                    }
                                }
                            }
                        },
                        Orders = { new OrderExpression("name", OrderType.Ascending) }
                    };

                    var result = service.RetrieveMultiple(qe).Entities;
                    var workflows = result.Select(e => new Flow(e, parent, settings)).ToList();
                    var childFlows = GetChildFlowsOnly(workflows, settings);
                    var childFlowsWithParents = RetrieveParentFlows(workflows, childFlows, settings);

                    return childFlowsWithParents;
                }

                return new List<Flow>();
            }
            catch (Exception error)
            {
                throw new Exception($"An exception occured while retrieving flows: {error.Message}");
            }
        }

        public static string UpdateSelectedFlows(MyPluginControl parent, IOrganizationService service, Flow childFlow, Settings settings)
        {
            // Create a list to hold the individual requests
            var requests = new OrganizationRequestCollection();

            if (!childFlow.NoChildActionRequired)
            {
                childFlow.record["clientdata"] = UpdateChildTrigger(childFlow.Name, childFlow.Content, settings.ChildTriggerInputName);

                var updateChildRequest = new UpdateRequest() { Target = childFlow.record, ConcurrencyBehavior = ConcurrencyBehavior.IfRowVersionMatches };
                requests.Add(updateChildRequest);
            }

            foreach (var parentFlow in childFlow.ParentFlows)
            {
                if (!parentFlow.NoParentActionRequired)
                {
                    parentFlow.record["clientdata"] = UpdateParentAction(parentFlow.Name, parentFlow.Content, childFlow.Id.ToString(), settings.ChildTriggerInputName);

                    var updateParentRequest = new UpdateRequest() { Target = parentFlow.record, ConcurrencyBehavior = ConcurrencyBehavior.IfRowVersionMatches };
                    requests.Add(updateParentRequest);
                }
            }

            if (requests.Count > 0)
            {
                // Prepare the request
                ExecuteTransactionRequest request = new ExecuteTransactionRequest()
                {
                    Requests = requests,
                    ReturnResponses = true
                };

                // Send the request
                var response = (ExecuteTransactionResponse)service.Execute(request);
                return $"Successfully updated selected flows";
            }

            return "No update is performed";
        }

        private static string UpdateChildTrigger(string flowName, string flowContent, string childTriggerInputName)
        {
            try
            {
                JObject jsonObj = JObject.Parse(flowContent);

                var properties = jsonObj["properties"]["definition"]["triggers"]["manual"]["inputs"]["schema"]["properties"] as JObject;

                bool containsParentFlowUrl = false;

                foreach (var property in properties.Properties())
                {
                    if (property.Value["title"] != null && property.Value["title"].ToString() == childTriggerInputName)
                    {
                        containsParentFlowUrl = true;
                        break;
                    }
                }

                if (!containsParentFlowUrl)
                {
                    properties.Add(childTriggerInputName, new JObject
                    {
                        { "title", childTriggerInputName },
                        { "type", "string" },
                        { "x-ms-dynamically-added", true },
                        { "description", "Please enter current flow run url" },
                        { "x-ms-content-hint", "TEXT" },
                    });

                    // Navigate to the "required" array
                    JArray requiredArray = (JArray)jsonObj["properties"]["definition"]["triggers"]["manual"]["inputs"]["schema"]["required"];

                    // Add new required properties
                    requiredArray.Add(childTriggerInputName);
                }

                string updatedJson = jsonObj.ToString();

                return updatedJson;
            }
            catch (Exception error)
            {
                throw new Exception($"An exception occured while updating {flowName} child trigger content: {error.Message}");
            }
        }

        private static string UpdateParentAction(string flowName, string flowContent, string childWorkflowId, string childTriggerInputName)
        {
            try
            {
                string parentFlowUrlValue = "@concat('https://make.powerautomate.com/environments/',workflow()['tags']['environmentName'],'/flows/shared/',workflow()['tags']['logicAppName'],'/runs/',workflow()?['run']['name'])";

                JObject jsonObj = JObject.Parse(flowContent);

                var actions = jsonObj["properties"]["definition"]["actions"];
                foreach (var action in actions)
                {
                    var inputs = action.First["inputs"];
                    if (inputs["host"] != null &&
                        inputs["host"]["workflowReferenceName"] != null &&
                        inputs["host"]["workflowReferenceName"]?.ToString() == childWorkflowId)
                    {
                        var body = inputs["body"] as JObject;
                        bool containsChildTriggerInputName = false;

                        foreach (var property in body.Properties())
                        {
                            if (property.Name == childTriggerInputName)
                            {
                                if (property.Value.ToString() == parentFlowUrlValue)
                                {
                                    containsChildTriggerInputName = true;
                                }
                            }
                        }

                        if (!containsChildTriggerInputName)
                        {
                            body[$"{childTriggerInputName}"] = parentFlowUrlValue;
                        }
                    }
                }

                string updatedJson = jsonObj.ToString();

                return updatedJson;
            }
            catch (Exception error)
            {
                throw new Exception($"An exception occured while updating {flowName} parent action : {error.Message}");
            }
        }

        private static IEnumerable<Flow> RetrieveParentFlows(IEnumerable<Flow> workflows, IEnumerable<Flow> childFlows, Settings settings)
        {
            try
            {
                List<Entity> matchingWorkflows = new List<Entity>();

                foreach (var childFlow in childFlows)
                {
                    foreach (var workflow in workflows)
                    {
                        FindNodesWithWorkflowId(childFlow, workflow, settings);
                    }
                }

                return childFlows;
            }
            catch (Exception error)
            {
                throw new Exception($"An exception occured while retrieving flows: {error.Message}");
            }
        }

        private static IEnumerable<Flow> GetChildFlowsOnly(IEnumerable<Flow> flows, Settings settings)
        {
            return flows
            .Where(flow => HasManualTrigger(flow.Content))
            .Select(flow =>
            {
                JObject jobject = JObject.Parse(flow.Content);
                var properties = jobject["properties"]["definition"]["triggers"]["manual"]["inputs"]["schema"]["properties"] as JObject;

                foreach (var property in properties.Properties())
                {
                    if (property.Value["title"] != null && property.Value["title"].ToString() == settings.ChildTriggerInputName)
                    {
                        flow.NoChildActionRequired = true;
                    }
                }
                flow.IsChild = true;
                return flow;
            });
        }

        private static bool IsValidJson(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            content = content.Trim();
            if ((content.StartsWith("{") && content.EndsWith("}")) || // For object
                (content.StartsWith("[") && content.EndsWith("]")))   // For array
            {
                try
                {
                    JToken.Parse(content);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }
            return false;
        }

        private static bool HasManualTrigger(string content)
        {
            if (IsValidJson(content))
            {
                JObject jobject = JObject.Parse(content);
                JToken manualTrigger = jobject.SelectToken("properties.definition.triggers.manual");
                return manualTrigger != null;
            }

            return false;
        }

        private static void FindNodesWithWorkflowId(Flow childFlow, Flow workflow, Settings settings)
        {
            string parentFlowUrlValue = "@concat('https://make.powerautomate.com/environments/',workflow()['tags']['environmentName'],'/flows/shared/',workflow()['tags']['logicAppName'],'/runs/',workflow()?['run']['name'])";
            string clientData = workflow.Content;

            if (!string.IsNullOrEmpty(clientData) && IsValidJson(clientData))
            {
                JObject jsonObject = JObject.Parse(clientData);

                // Navigate to the "actions" node within "rootNode"
                var actionsNode = jsonObject["properties"]?["definition"]?["actions"];
                if (actionsNode != null)
                {
                    var actions = actionsNode.Children().Select(action => action.First).ToList();

                    // Check if all actions satisfy the conditions
                    var filteredActions = actions
                    .Where(action =>
                    {
                        // Check if "inputs" is a JObject
                        if (action["inputs"] is JObject inputs)
                        {
                            // Check if "host" is a JObject
                            if (inputs["host"] is JObject host)
                            {
                                // Check if "workflowReferenceName" is a JValue and not null
                                if (host["workflowReferenceName"] is JValue workflowReferenceName && workflowReferenceName != null)
                                {
                                    return workflowReferenceName.ToString() == childFlow.Id.ToString();
                                }
                            }
                        }
                        return false;
                    })
                    .ToList();

                    if (filteredActions.Any())
                    {
                        Flow newParentFlow = new Flow(workflow.record, workflow.Plugin, workflow.settings);

                        if (jsonObject.SelectToken("properties.definition.triggers.manual") != null)
                        {
                            newParentFlow.IsChild = true;
                        }

                        bool allActionsSatisfied = filteredActions.Any() && filteredActions
                            .All(action =>
                            {
                                var inputs = action["inputs"];
                                var host = inputs?["host"];
                                var body = inputs?["body"] as JObject;
                                return host != null &&
                                       body != null &&
                                       body[settings.ChildTriggerInputName]?.ToString() == parentFlowUrlValue;
                            });

                        if (allActionsSatisfied)
                        {
                            newParentFlow.NoParentActionRequired = true;
                        }

                        if (filteredActions.Any() && !childFlow.ParentFlows.Any(f => f.Id == workflow.Id))
                        {
                            childFlow.ParentFlows.Add(newParentFlow);
                        }
                    }
                }
            }
        }
    }
}
