using Entity;
using MessangerServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly Logger<MessageController> log;

        private User CurrentUser => _userRepository
            .FindById(int.Parse(User.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value));

        public MessageController(MessageService messageService, Repository<User> userRepository, GroupService groupService, Logger<MessageController> log)
        {
            _messageService = messageService;
            _userRepository = userRepository;
            _groupService = groupService;
            this.log = log;
        }

        [HttpPost("create/{groupId}")]
        public IActionResult CreateMessage(int groupId, [FromBody] Message message)
        {
            Message sendedMessage = null;
            try
            {
                sendedMessage = _messageService.SendMessage(CurrentUser, _groupService.GetById(groupId), message.Body);
            } catch (Exception e)
            {
                log.LogInformation("Умер");
            }
            return Ok(sendedMessage);
        }
    }
}
