namespace DynamicWorkflow.Core;

public class Instance
{
    string Id;
    string DefinitionId;
    state CurrentState;
    List<Record> History;
}
