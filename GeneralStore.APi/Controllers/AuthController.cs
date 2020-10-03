using AutoMapper;
using GeneralStore.Api.Config;
using GeneralStore.Api.Dtos;
using GeneralStore.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private ILogger<AuthController> _logger;
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private JwtConfig _jwtSetting;

        public AuthController(ILogger<AuthController> logger, UserManager<User> userManager, IMapper mapper, IOptionsSnapshot<JwtConfig> jwtSetting)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtSetting = jwtSetting.Value;
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> UserSignUp([FromBody] UserSignUpDto userData)
        {
            var user = _mapper.Map<UserSignUpDto, User>(userData);
            var userResult = await _userManager.CreateAsync(user, userData.Password);

            if (userResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "customer");
                return Created(string.Empty, string.Empty);
            }

            return Problem(userResult.Errors.First().Description, null, 500);
        }

        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UserSignIn([FromBody] UserSignInDto userData)
        {
            var user = await _userManager.FindByEmailAsync(userData.email);
            if (user is null)
            {
                return BadRequest($"User with e-mail address {userData.email} doesn't exist");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, userData.password);
            if (passwordValid)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var jwtToken = GenerateJwt(user, roles);
                return Ok(jwtToken);
            }
            else
            {
                return BadRequest("Wrong password");
            }
        }

        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSetting.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
