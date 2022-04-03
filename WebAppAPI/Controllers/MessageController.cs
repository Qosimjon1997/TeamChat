using AutoMapper;
using BusinessLayer.Interfaces;
using DataLayer.Dtos.MessageDtos;
using DataLayer.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getallmessagesfromusers")]
        public async Task<ActionResult<IEnumerable<MessageReadDto>>> GetAllMessagesFromUsers(Guid fromId, Guid toId)
        {
            var messageItems = await _messageService.GetAllMessagesFromUser(fromId, toId);
            return Ok(_mapper.Map<IEnumerable<MessageReadDto>>(messageItems));
        }

        
        [HttpPost]
        [Route("addmessage")]
        public async Task<ActionResult<MessageReadDto>> AddMessage(MessageCreateDto messageCreateDto)
        {
            var messageModel = _mapper.Map<Message>(messageCreateDto);
            bool answare = await _messageService.AddMessage(messageModel);
            if (answare)
            {
                return Ok(messageModel);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
