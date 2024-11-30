using Microsoft.AspNetCore.Mvc;
using btlz.Models;
using btlz.Exceptions;

namespace SimpleExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private static readonly List<User> Users = new()
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

    [HttpGet]
    public ActionResult<List<User>> GetUsers() => Ok(Users);

    [HttpGet("{login}")]
    public ActionResult<User?> GetUser(string login) 
        => Ok(Users.FirstOrDefault(user => user.Login == login));
    
    [HttpPost]
    public ActionResult<int> AddUser(string login, [FromBody] string password)
    {
        var userId = Users.Count;
        Users.Add(new User
        {
            Id = userId,
            Login = login,
            Password = password
        });
    
        // В Ok() можно ничего и не передавать, но тогда будет пустой ответ.
        return Ok(userId);
    }
    
    [HttpPut]
    public ActionResult UpdateUser(int id, string login, [FromBody] string password)
    {
        var user = TryGetUserAndThrowIfNotFound(id);
    
        user.Login = login;
        user.Password = password;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = TryGetUserAndThrowIfNotFound(id);

        Users.Remove(user);

        return NoContent();
    }
    
    private User TryGetUserAndThrowIfNotFound(int id)
    {
        var user = Users.FirstOrDefault(user => user.Id == id);
        if (user is null)
        {
            throw new UserNotFoundException(id);
        }

        return user;
    }
}