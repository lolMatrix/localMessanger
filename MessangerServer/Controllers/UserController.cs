using MessangerServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessangerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private ILogger<UserController> _log;

        public UserController(UserService service, ILogger<UserController> log)
        {
            _userService = service;
            _log = log;
        }

        [HttpPost("login")]
        public IActionResult Authorize([FromBody] Login login)
        {
            string token;
            try
            {
                token = _userService.Auth(login);
            } 
            catch(NullReferenceException e)
            {
                _log.LogError("Пользователь не найден: {0}", e.Message);
                return Unauthorized(new
                {
                    err = e.Message
                });
            }

            return Ok(new
            {
                token = token
            });
        }

        [HttpPut("reg")]
        public IActionResult Registration([FromBody] Login reg)
        {
            string token;
            try
            {
                token = _userService.Registration(reg);
            }
            catch (ArgumentException e)
            {
                _log.LogError("Ошибка создания пользователя: {0}", e.Message);
                return Unauthorized(new
                {
                    err = e.Message
                });
            }

            return Ok(new
            {
                token = token
            });
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAll());
        }
    }
}
