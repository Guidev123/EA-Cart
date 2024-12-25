using Cart.API.Configurations;
using Cart.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddApplicationConfig();
builder.AddJwtConfiguration();
builder.AddDocumentationConfig();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseSecurity();

app.MapEndpoints();

app.Run();
