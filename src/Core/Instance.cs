namespace DynamicWorkflow.Core;

public class Instance
{
    string Id;
    string DefinitionId;
    State CurrentState;
    List<Record> History;
}
