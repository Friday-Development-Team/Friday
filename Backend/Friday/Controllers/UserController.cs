using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers {
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase {

        private readonly IUserService service;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public UserController(IUserService service, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration config) {
            this.service = service;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
        }


        /// <summary>
        /// Returns the currently logged in user based on the provided token
        /// </summary>
        /// <returns>Information of the User</returns>
        [HttpGet]
      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ShopUserDTO Get() {
            var name = User.Identity.Name;
            var result = service.GetUser(name);
            return result;
        }

        [HttpGet("all")]
       // [Authorize(Roles = Role.Admin)]
        public IList<ShopUserDTO> GetAll() {
            var users = service.GetAll();
            return users;
        }

        /// <summary>
        /// Gets the roles of the currently logged in user
        /// </summary>
        /// <returns>List of roles</returns>
        [HttpGet("roles")]
        public async Task<IList<string>> GetRolesAsync() {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var temp = await userManager.GetRolesAsync(user);
            return temp;
        }
        /// <summary>
        /// Checks if the provided username already exists
        /// </summary>
        /// <param name="name">Name to be checked</param>
        /// <returns>True if doesn't exist</returns>
        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string name) {
            return await userManager.FindByNameAsync(name) == null;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">Model containing login information</param>
        /// <returns>JWT</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<String>> CreateToken([FromBody] LoginDTO model) {
            var name = model.Username;
            var user = await userManager.FindByNameAsync(name);
            if (user != null) {
                var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded) {
                    string token = await GetToken(user);
                    return Created("", token); //returns only the token

                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Registers a new User.
        /// </summary>
        /// <param name="model">Object containing information needed to register</param>
        /// <returns>JWT</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        //[Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<String>> Register(RegisterDTO model) {
            IdentityUser user = new IdentityUser { UserName = model.Username };
            ShopUser customer = new ShopUser { Name = model.Username, Balance = 200D };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {
                if (service.AddUser(customer)) {
                    string token = await GetToken(user);
                    return Created("", token);
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Adds Balance to the specified user's account
        /// </summary>name">Name of the User</param>
        /// <param name="amount">Amount to be added. Negative to subtract</param>
        [HttpPut("updatebalance")]
      //
        [Authorize(Roles = Role.Admin)]
        public void UpdateBalance([FromBody] BalanceUpdateDTO dto) {
            service.ChangeBalance(dto.Name, dto.Amount, true);
        }

        private async Task<string> GetToken(IdentityUser user) {
            // Create the token
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName) };
            claims.AddRange((await userManager.GetRolesAsync(user)).Select(s => new Claim(ClaimTypes.Role, s)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, null,
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
