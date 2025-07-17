using DynamicWorkflow.Core;

namespace DynamicWorkflow.Store;

public class DefinitionStore
{
    Dictionary<string, Definition> definitions;

    public void Save(Definition definition)
    {
        definitions[definition.Id] = definition;
    }

    public Definition? Get(string id)
    {
        definitions.TryGetValue(id, out var definition);
        return definition;
    }
}
