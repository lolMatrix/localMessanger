using Entity;
using MessangerServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Linq;
using System.Security.Claims;

namespace MessangerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly Repository<User> _userRepository;
        private readonly GroupService _groupService;

        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        private User CurrentUser => _userRepository.FindById(UserId);

        public GroupController(Repository<User> userRepository, GroupService groupService)
        {
            _userRepository = userRepository;
            _groupService = groupService;
        }

        [HttpPost("create")]
        public IActionResult CreateGroup([FromBody] string name)
        {
            var group = _groupService.CreateGroup(name, CurrentUser);
            return Ok(group);
        }

        [HttpPost("add-user/{id}")]
        public IActionResult AddUser(int id, [FromBody] int userId)
        {
            var group = _groupService.AddUserToGroup(_userRepository.FindById(userId), id);
            return Ok(group);
        }

    }
}
