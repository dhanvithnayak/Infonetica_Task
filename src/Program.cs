using DynamicWorkflow.Core;

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
app.MapPost("/workflow", (Definition definition) =>
{
    try
    {
        definition.Validate();  // TODO
        // TODO save in store
        return Results.Ok();
    }
    catch (System.Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

// Get a workflow configuration
app.MapGet("/workflow/{id}", (string id) =>
{
    // TODO get from store
    // var definition = <getfromstore>
    
    return definition is not null
        ? Results.Ok(definition)
        : Results.NotFound();
});

// Start a workflow given ID
app.MapPost("/workflow/{id}/start", () =>
{
    // TODO get definition from store
    // var definition = <getfromstore>
    
    if(definition is null)
        return Results.NotFound();

    var initial_state = definition.States.FirstOrDefault(s => s.IsInitial);

    var instance = new Instance
    {
        Id = Guid.NewGuid().ToString(),
        DefinitionId = id,
        CurrentState = initial_state.Id,
    };

    // TODO store Instance
    
    return Results.Created($"/instance/{instance.Id}", instance);
});

// Retrieve instance state and history
app.MapGet("/instance/{id}", () => 
{
    // TODO get instance from store

    return instance is not null
        ? Results.Ok(instance)
        : Results.NotFound();
});

// Execute an action
app.MapPost("/instance/{id}/action", (string action_id) => 
{
    // TODO get instance from store
    // var instance = loremepsum
    
    if(instance is null)
        return Results.NotFound();

    // TODO get definition
    // var definition =
    
    var currentState = definition.States.FirstOrDefault(s => s.Id == instance.CurrentState);

    var action = currentState.Actions.FirstOrDefault(a => a.Id == action_id);
    if(action is null)
        return Results.BadRequest();

    // TODO save instance

    return Results.Ok(instance);
});

app.Run();
