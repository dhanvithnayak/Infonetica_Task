using System.ComponentModel.DataAnnotations;

namespace DynamicWorkflow.Core;

public class Action
{
    public string Id;
    public string ToState;
    public List<string> FromStates = new();
    public bool Enabled;

    public void Validate(HashSet<string> validStateIds)
    {
        if(string.IsNullOrWhiteSpace(Id))
            throw new ValidationException("Action ID necessary");

        if(!validStateIds.Contains(ToState))
            throw new ValidationException($"Target state '{ToState}' not defined");

        foreach(var state in FromStates)
        {
            if(!validStateIds.Contains(state))
                throw new ValidationException($"From state '{state}' not defined");
        }
    }
}
