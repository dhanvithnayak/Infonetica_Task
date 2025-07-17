var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Create a new workflow in one go
app.MapPost("/workflow", () => {});
// Get a workflow configuration
app.MapGet("/workflow/{id}", () => {}); 
// Start a workflow given ID
app.MapGet("/workflow/{id}/start", () => {});

// Retrieve instance state and history
app.MapGet("/instance/{id}", () => {});
// Execute an action
app.MapPost("/instance/{id}/action", () => {});

app.Run();
