namespace DynamicWorkflow.Core;

public class Action
{
    public string Id;
    public string ToState;
    public List<string> FromStates = new();
    public bool Enabled;
}
