using System.ComponentModel.DataAnnotations;
using DynamicWorkflow.Core;
using DynamicWorkflow.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// The API endpoints below can be moved into an Endpoints subfolder for better structuring
// but since our API is very minimal, I'll go ahead with writing it inline here

// Create a new workflow in one go
app.MapPost("/workflow", (Definition definition, DefinitionStore store) =>
{
    try
    {
        definition.Validate();
        store.Save(definition);
        return Results.Ok();
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

// Get a workflow configuration
app.MapGet("/workflow/{id}", (string id, DefinitionStore store) =>
{
    var definition = store.Get(id);
    
    return definition is not null
        ? Results.Ok(definition)
        : Results.NotFound();
});

// Start a workflow given ID
app.MapPost("/workflow/{id}/start", (string id, DefinitionStore definitionStore, InstanceStore instanceStore) =>
{
    var definition = definitionStore.Get(id);

    if(definition is null)
        return Results.NotFound();

    var initialState = definition.States.FirstOrDefault(s => s.IsInitial)!;

    var instance = new Instance
    {
        Id = Guid.NewGuid().ToString(),
        DefinitionId = id,
        CurrentState = initialState,
    };

    instanceStore.Save(instance);
    
    return Results.Created($"/instance/{instance.Id}", instance);
});

// Retrieve instance state and history
app.MapGet("/instance/{id}", (string id, InstanceStore store) => 
{
    var instance = store.Get(id);

    return instance is not null
        ? Results.Ok(instance)
        : Results.NotFound();
});

// Execute an action
app.MapPost("/instance/{id}/action", (string id, string actionId, DefinitionStore definitionStore, InstanceStore instanceStore) => 
{
    var instance = instanceStore.Get(id);
    
    if(instance is null)
        return Results.NotFound();

    var definition = definitionStore.Get(instance.DefinitionId);
    var currentState = definition.States.FirstOrDefault(s => s.Id == instance.CurrentState.Id)!;
    var action = definition.Actions.FirstOrDefault(a => a.Id == actionId && a.Enabled && a.FromStates.Contains(currentState.Id))!;

    if(action is null)
        return Results.BadRequest();

    instance.CurrentState = definition.States.FirstOrDefault(s => s.Id == action.ToState)!;

    instanceStore.Save(instance);

    return Results.Ok(instance);
});

app.Run();
