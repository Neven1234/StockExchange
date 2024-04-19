
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockDomainLayer.Models;
using StockExchange.DTOs;
using StockServiceLayer.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IAuth auth,IMapper mapper,IConfiguration configuration)
        {
            _auth = auth;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task <IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var userFromRepo=await _auth.Login(userLoginDTO.UserName, userLoginDTO.Password);
            if(userFromRepo == null)
            {
                return BadRequest("Username or password are wrong");
            }
            var authClaims = new List<Claim>
            {
                new Claim("name",userFromRepo.UserName),
                new Claim("userId",userFromRepo.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var jwtToken = getToken(authClaims);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var userToreturn = _mapper.Map<UserToReturnDTO>(userFromRepo);

            return Ok(token);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRigisterDTO userRigisterDTO)
        {
            var result=await _auth.Register(userRigisterDTO.UserName, userRigisterDTO.Password,userRigisterDTO.Email);
            return Ok(result);
        }

        //helper function
        private JwtSecurityToken getToken(List<Claim> authClims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: this._configuration["JWT:ValidIssuer"],
                audience: this._configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return token;

        }
    }
}
