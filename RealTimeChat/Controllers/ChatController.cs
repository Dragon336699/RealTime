using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RealTimeChat.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ChatController(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //[Route("user/listChats")]
        //[Authorize]
        //public async Task<IActionResult> GetUsersListChats (int userId)
        //{
            
        //}
    }
}
