using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using System.Web;

namespace RealTimeChat.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public UserController(IConfiguration configuration, UserManager<User> userManager, IMapper mapper, RoleManager<Role> roleManager, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("user/register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistration userModel)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!ModelState.IsValid)
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
                        return BadRequest(ModelState);
                    }


                    if (!await _roleManager.RoleExistsAsync("user"))
                    {
                        var role = new Role { Name = "user" };
                        var check = await _roleManager.CreateAsync(role);
                        if (!check.Succeeded)
                        {
                            return BadRequest(new { message = "Create Role fail" });
                        }
                    }

                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string encodedToken = WebUtility.UrlEncode(token);
                    var confirmationLink = $"{Request.Scheme}://{Request.Host}/api/v1/user/confirmEmail?token={encodedToken}&email={user.Email}";
                    await _emailSender.SendEmailAsync(user.Email, "Confirmation email link", $"Please confirm your email by clicking on this link: <a href='{confirmationLink}'>Click this link to confirm your email</a>");

                    await _userManager.AddToRoleAsync(user, "user");

                    transaction.Complete();

                    return Ok(new { message = "Register Successful" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = "Register fail" });
                }
            }
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
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            if (!user.EmailConfirmed)
            {
                return Unauthorized(new { message = "Your email has not been confirmed." });
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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

        [HttpGet]
        [Route("user/find")]
        [Authorize]
        public async Task<IActionResult> FindUserByName(string searchContent)
        {
            var users = _unitOfWork.Users.Find(u => (u.LastName + " " + u.FirstName).ToLower().Contains(searchContent.ToLower()));
            var userDtos = users.Select(u => _mapper.Map<UserInforDto>(u)).ToList();

            return Ok(userDtos);
        }

        [HttpGet]
        [Route("user/findById")]
        [Authorize]
        public async Task<IActionResult> FindUserById(Guid id)
        {
            var user = _unitOfWork.Users.GetById(id);
            var userDto = _mapper.Map<UserInforDto>(user);

            return Ok(userDto);
        }

            [HttpGet]
            [Route("user/confirmEmail")]
            public async Task<IActionResult> ConfirmEmail(string token, string email)
            {
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                {
                    return Content("Invalid token or mail");
                }

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return Content("User not found");
                }

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return Content("Confirm Email Successful!");
                }
                return Content("Confirm Email Failed, please try again.");
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
