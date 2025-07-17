namespace DynamicWorkflow.Core;

public class Action
{
    string Id;
    string ToState;
    List<string> FromStates;
    bool Enabled;
}
