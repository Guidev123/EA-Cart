using Cart.API.Configurations;
using Cart.API.Endpoints;
using SharedLib.Tokens.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddApplicationConfig();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorizationBuilder();
builder.AddDocumentationConfig();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthConfiguration();

app.MapEndpoints();

app.Run();
