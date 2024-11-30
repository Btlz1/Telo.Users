using btlz.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();

app.UseSwaggerUI();
var users = new List<User>
{
    new()
    {
        Id = 0,
        Login = "Ozon671Games",
        Password = "Boyarin"
    },
    new()
    {
        Id = 1,
        Login = "Vasya",
        Password = "Zver9999"
    }
};
app.MapPost("/user", (string login, [FromBody] string password) 
    => users.Add(new User
    {
        Id = users.Count,
        Login = login,
        Password = password
    }));
app.MapPut("/user", (int id, string login, [FromBody] string password) =>
{
    var userForUpdate = users.FirstOrDefault(user => user.Id == id);
    if (userForUpdate is null)
    {
        return Results.NotFound();
    }

    userForUpdate.Login = login;
    userForUpdate.Password = password;

    return Results.NoContent();
});


app.MapGet("/user", () => users);
app.MapDelete("/user/{id}", (int id) =>
{
    var userToRemove = users.FirstOrDefault(user => user.Id == id);
    if (userToRemove is null)
    {
        return Results.NotFound();
    }

    users.Remove(userToRemove);
    
    return Results.NoContent();
}); 
app.Run();