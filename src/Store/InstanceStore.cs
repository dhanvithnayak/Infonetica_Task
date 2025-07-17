using DynamicWorkflow.Core;

namespace DynamicWorkflow.Store;

public class InstanceStore
{
    Dictionary<string, Instance> instances;

    public void Save(Instance instance)
    {
        instances[instance.Id] = instance;
    }

    public Instance? Get(string id)
    {
        instances.TryGetValue(id, out var instance);
        return instance;
    }
}
