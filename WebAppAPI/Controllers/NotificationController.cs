using AutoMapper;
using BusinessLayer.Interfaces;
using DataLayer.Dtos.NotificationDtos;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(IMapper mapper, INotificationService notificationService)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getallnotifications")]
        public async Task<ActionResult<IEnumerable<NotificationReadDto>>> GetAllNotifications()
        {
            var notificationItems = await _notificationService.GetAllNotifications();
            return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notificationItems));
        }

        /*[HttpGet]
        [Route("getnotificationbyid")]
        public async Task<ActionResult<IEnumerable<NotificationReadDto>>> GetNotificationById(Guid id)
        {
            var notificationItems = await _notificationService.GetAllNotifications();
            return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notificationItems));
        }*/

        [HttpPost]
        [Route("addnotification")]
        public async Task<ActionResult<NotificationReadDto>> AddNotification(NotificationCreateDto notificationCreateDto)
        {
            var notificationModel = _mapper.Map<Notification>(notificationCreateDto);
            await _notificationService.AddNotification(notificationModel);
            return Ok(notificationModel);
        }
    }
}
