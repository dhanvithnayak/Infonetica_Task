namespace DynamicWorkflow.Core;

public class Definition
{
    public string Id;
    public List<State> States = new();
    public List<Action> Actions = new();
}
