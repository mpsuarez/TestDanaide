using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDanaide.Models;
using TestDanaide.Persistence.Entities;
using TestDanaide.Services;
using TestDanaide.Services.Interfaces;

namespace TestDanaide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.CreateUserAsync(createUser.DNI);
                if (user.IsSuccess)
                {
                    CreateUserResponse createUserResponse = new()
                    {
                        UserId = user.Value.Id,
                    };
                    return Ok(createUserResponse);
                }
                else
                {
                    return BadRequest(user.Errors);
                }
            }
            return BadRequest(createUser);
        }

        [HttpGet("MostExpensiveProducts/{dni}")]
        public async Task<IActionResult> GetMostExpensiveProductsByUserDNI(string dni)
        {
            Result<IList<Product>> result = await _userService.GetMostExpensiveProductsBought(dni);
            if (result.IsSuccess)
            {
                return Ok(result.Value.Select(p => new 
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }));
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
}
