using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Features.Commands.Chat;
using System.Runtime.InteropServices;

namespace RealTimeChat.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediatr;
        public ChatController(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediatr)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediatr = mediatr;
        }

        [HttpPost]
        [Route("chat/createChat")]
        [Authorize]
        public async Task<IActionResult> CreateChat(CreateChatDto createChat)
        {
            Guid newChatId;
            if (createChat.isGroup) {
                newChatId = await _mediatr.Send(new CreateChat(createChat.usersId, createChat.roles, createChat.isGroup, "Default group chat"));
            } else
            {
                newChatId = await _mediatr.Send(new CreateChat(createChat.usersId, createChat.roles, createChat.isGroup, null));
            }
            if (newChatId == Guid.Empty)
            {
                return BadRequest(new
                {
                    message = "Create chat fail"
                });
            }
            return Ok(new
            {
                message = "Create chat success",
                chatId = newChatId
            });
        }
    }
}
