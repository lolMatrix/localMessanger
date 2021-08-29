using Entity;
using MessangerServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MessangerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;
        private readonly GroupService _groupService;
        private readonly Repository<User> _userRepository;

        private User CurrentUser => _userRepository
            .FindById(int.Parse(User.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value));

        public MessageController(MessageService messageService, Repository<User> userRepository, GroupService groupService)
        {
            _messageService = messageService;
            _userRepository = userRepository;
            _groupService = groupService;
        }

        [HttpPost("create/{groupId}")]
        public IActionResult CreateMessage(int groupId, [FromBody] Message message)
        {
            var sendedMessage = _messageService.SendMessage(CurrentUser, _groupService.GetById(groupId), message.Body);
            return Ok(sendedMessage);
        }
    }
}
