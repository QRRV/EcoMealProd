
using System.Diagnostics;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using HotChocolate.AspNetCore;
using MealBoxAPI.Models;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the database context
var primaryConnectionString = builder.Configuration.GetConnectionString("PrimaryDatabase");
Debug.Assert(primaryConnectionString != null, nameof(primaryConnectionString) + " != null");
Console.WriteLine("Primary DB Connection String in Program.cs:");
Console.WriteLine(primaryConnectionString);
builder.Services.AddDbContext<SqlServerDb>(options => 
    options.UseSqlServer(primaryConnectionString));



// Enable Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IMealBoxRepository, MealBoxRepository>();

// Configure GraphQL with HotChocolate
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>(); 

var app = builder.Build();

// Enable Swagger for all environments (including production)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    options.RoutePrefix = "swagger"; 
});

// Enable GraphQL and GraphiQL
app.MapGraphQL("/graphql");


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();