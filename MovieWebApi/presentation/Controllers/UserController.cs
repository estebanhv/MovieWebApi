using Microsoft.AspNetCore.Mvc;
using MovieWebApi.presentation.dto;
using MovieWebApi.service.implementation;
using MovieWebApi.service.interfaces;

namespace MovieWebApi.presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase 
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO dto)
        {
            var result = await _userService.RegisterAsync(dto);
            return result ? Ok() : BadRequest("Email ya está en uso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO dto)
        {
            var token = await _userService.LoginAsync(dto);
            return token != null ? Ok(token) : Unauthorized("Credenciales inválidas.");
        }
    }
}
