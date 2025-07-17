using System.ComponentModel.DataAnnotations;

namespace DynamicWorkflow.Core;

public class Definition
{
    public string Id;
    public List<State> States = new();
    public List<Action> Actions = new();

    public void Validate()
    {
        if(string.IsNullOrWhiteSpace(Id))
            throw new ValidationException("Workflow ID required");

        if(States == null || States.Count == 0)
            throw new ValidationException("At least one state required");

        var stateIds = new HashSet<string>();
        foreach(var state in States)
        {
            state.Validate();

            if(!stateIds.Add(state.Id))
                throw new ValidationException($"Duplication state ID '{state.Id}' in workflow definition '{Id}'");
        }

        var actionIds = new HashSet<string>();
        foreach(var action in Actions)
        {
            action.Validate(stateIds);

            if(!actionIds.Add(action.Id))
                throw new ValidationException($"Duplication action ID '{action.Id}' in workflow definition '{Id}'");
        }

        var initialStates = States.Count(s => s.IsInitial);
        if(initialStates != 1)
            throw new ValidationException("Exactly one initial state must be defined");
    }
}
