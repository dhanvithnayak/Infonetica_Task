namespace DynamicWorkflow.Core;

public class Instance
{
    public string Id;
    public string DefinitionId;
    public State CurrentState;
    public List<Record> History = new();
}
