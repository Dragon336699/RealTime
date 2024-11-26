using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Features.Commands.Chat;

namespace RealTimeChat.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediatr;
        public MessageController(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediatr)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediatr = mediatr;
        }

        [HttpPost]
        [Route("message/saveMessage")]
        [Authorize]
        public async Task<IActionResult> SaveMessage(SaveMessageDto messageInfor)
        {
            _unitOfWork.Messages.Add(_mapper.Map<Message>(messageInfor));
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
