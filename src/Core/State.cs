using System.ComponentModel.DataAnnotations;

namespace DynamicWorkflow.Core;

public class State
{
    public string Id;
    public bool IsInitial;
    bool IsFinal;
    bool Enabled;

    public void Validate()
    {
        if(string.IsNullOrWhiteSpace(Id))
            throw new ValidationException("State ID required");
    }
}
