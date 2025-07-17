namespace DynamicWorkflow.Core;

public class Instance
{
    public string Id;
    public string DefinitionId;
    public State CurrentState;
    List<Record> History = new();
}
