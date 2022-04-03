using AutoMapper;
using BusinessLayer.Hubs;
using BusinessLayer.IHubs;
using DataLayer.Dtos.ApplicationUserDtos;
using DataLayer.Dtos.MessageDtos;
using DataLayer.IRepositories;
using DataLayer.Models;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        private readonly IRepository<Message> _message;
        private readonly IUpdateMessageRepo<Message> _updateMessage;

        public AuthenticateController(IUpdateMessageRepo<Message> updateMessage,IRepository<Message> message,IHubContext<ChatHub, IChatHub> hubContext , IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _hubContext = hubContext;
            _message = message;
            _updateMessage = updateMessage;
        }

        [HttpGet]
        [Route("getallemployeeforadmin")]
        public async Task<ActionResult<IEnumerable<ApplicationUserReadDto>>> GetAllEmployeeForAdmin()
        {
            var users = await userManager.Users.ToListAsync();
            var finalusers = (from a in users
                              where a.RoleName != "Admin"
                              select a).ToList();
            return Ok(_mapper.Map<IEnumerable<ApplicationUserReadDto>>(finalusers));
        }

        [HttpGet]
        [Route("getallemployeeforuser")]
        public async Task<ActionResult<IEnumerable<ApplicationUserReadDto>>> GetAllEmployeeForUser(Guid id)
        {
            var users = await userManager.Users.ToListAsync();
            var finalusers = (from a in users
                            where a.RoleName != "Admin" && a.Id != id.ToString()
                            select a).ToList();
            return Ok(_mapper.Map<IEnumerable<ApplicationUserReadDto>>(finalusers));
        }

        [HttpGet]
        [Route("getallemployeeforuserwithmessages")]
        public async Task<ActionResult<IEnumerable<MessageAndUserDto>>> GetAllEmployeeForUserWithMessages(Guid id)
        {
            List<MessageAndUserDto> mydata = new List<MessageAndUserDto>();
            List<string> mm = new List<string>();
            var users = await userManager.Users.ToListAsync();
            var finalusers = (from a in users
                              where a.RoleName != "Admin" && Guid.Parse(a.Id) != id
                              select a).ToList();

            
            var messages = await _message.GetAllAsync();
            var finalMessages2 = (from item in messages
                                  where item.ToMessage == id && item.isRead == false
                                  group item by item.FromMessage into g
                                  select new
                                  {
                                      g.Key
                                  }).ToList();
            
            foreach(var v1 in finalMessages2)
            {
                mm.Add(v1.Key.ToString());
            }

            foreach (var v1 in finalusers)
            {
                MessageAndUserDto mock = new MessageAndUserDto();
                mock.Id = v1.Id;
                mock.FIO = v1.FIO;
                mock.UserName = v1.UserName;
                mock.isRead = mm.Contains(v1.Id.ToString());

                mydata.Add(mock);
            }
            await _hubContext.Clients.All.AllUser();
            mydata = (from a in mydata
                      orderby !a.isRead, a.FIO
                      select a).ToList();
            return mydata;
        }

        [HttpGet]
        [Route("getselecteduser")]
        public async Task<ActionResult<ApplicationUserReadDto>> GetSelectedUser(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return Ok(_mapper.Map<ApplicationUserReadDto>(user));
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            var generate_token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, generate_token, model.NewPassword);
            if(result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("updatemessagesofuser")]
        public async Task<IActionResult> UpdateMessagesOfUser(ForUsersMessageDto model)
        {
            var messages = await _message.GetAllAsync();
            var usermessages = (from a in messages
                                where a.ToMessage == model.toId && a.FromMessage == model.fromId && a.isRead == false
                                select a).ToList();

            if(usermessages.Count>0)
            {
                bool query = await _updateMessage.UpdateMessageWithList(usermessages);
                await _hubContext.Clients.All.AllUser();
                return Ok();
            }
            return Ok();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    fio = user.FIO,
                    roleName = user.RoleName,
                    id = user.Id,
                    username = user.UserName
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FIO = model.FIO,
                RoleName = UserRoles.User
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

            await _hubContext.Clients.All.AllUser();
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FIO = model.FIO,
                RoleName = UserRoles.Admin
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "Administrator created successfully!" });
        }

        [HttpDelete]
        [Route("userdelete")]
        public async Task<ActionResult<bool>> UserDelete(Guid id)
        {
            var userExists = await userManager.FindByIdAsync(id.ToString());
            if (userExists == null)
                return false;

            await userManager.DeleteAsync(userExists);
            await _hubContext.Clients.All.AllUser();
            return true;
        }

    }
}
