using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealTimeChat.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IMediator mediator, IConfiguration configuration, UserManager<User> userManager, IMapper mapper, RoleManager<Role> roleManager, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("user/register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistration userModel)
        {

            if  (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(userModel);

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(userModel);
            }

            if (!await _roleManager.RoleExistsAsync("user"))
            {
                var role = new Role { Name = "user" };
                var check = await _roleManager.CreateAsync(role);
                if (!check.Succeeded)
                {
                    return BadRequest(new { message = "Tạo vai trò không thành công" });
                }
            }

            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.AddToRoleAsync(user, "user");
            if (result.Succeeded)
            {
                return Ok(new { message = "Đăng ký thành công" });
            }

            return BadRequest(new { message = "Đăng ký thát bại" });
        }

        [HttpPost]
        [Route("user/login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in  userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token  = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    message = "Login successful!"
                });

            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("user/infor")]
        public async Task<IActionResult> GetInforUser(string token)
        {
            var user = _unitOfWork.Users.GetUserByToken(token);
            var userInfor = _mapper.Map<UserInforDto>(user);

            return Ok(userInfor);
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

    }
}
