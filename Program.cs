using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Swagger activé dans tous les environnements
app.MapOpenApi();
app.UseSwaggerUi(options =>
{
    options.DocumentPath = "/openapi/v1.json";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
