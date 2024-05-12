var builder = WebApplication.CreateBuilder(args);

{
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();
  builder.Services.AddControllers();

  // builder.Services.AddAuthentication();
  // builder.Services.AddAuthorization();
}

var app = builder.Build();

{
  // Configure the HTTP request pipeline.
  // if (app.Environment.IsDevelopment())
  // {
  app.UseSwagger();
  app.UseSwaggerUI();
  // }

  app.UseHttpsRedirection();
  app.MapControllers();
  app.MapGet("time/utc", () => Results.Ok(DateTime.UtcNow));
}

await app.RunAsync();