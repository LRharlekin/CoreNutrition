var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.Run();

app.MapGet("time/utc", () => Results.Ok(DateTime.UtcNow))
    .WithName("GetUtcTime")
    .WithOpenApi(operation => {
        operation.Summary = "Get the current UTC time";
        operation.Description = "This endpoint returns the current UTC time in ISO 8601 format.";
        operation.Responses[StatusCodes.Status200OK].Description = "The current UTC time";
        operation.Responses[StatusCodes.Status200OK].Content.Add(
            "text/plain",
            new OpenApiMediaType
            {
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(DateTime.UtcNow.ToString("O"))
                }
            });
    });

await app.RunAsync();