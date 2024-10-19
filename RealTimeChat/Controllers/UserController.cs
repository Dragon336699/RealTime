using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RealTimeChat.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IMapper

        public UserController(IMediator mediator, IConfiguration configuration, UserManager<User> userManager)
        {
            _mediator = mediator;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("user/register")]
        public async Task<IActionResult> RegisterUser()
        {

        }
    }
}
