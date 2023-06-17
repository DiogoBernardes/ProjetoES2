using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Interface;
using Backend.Models;
using BusinessLogic.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] AuthenticationModel login)
        {
            if (await IsValidUser(login))
            {
                var token = GenerateJwtToken(login.Email);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        private async Task<bool> IsValidUser(AuthenticationModel login)
        {   
            var user = await _userRepository.GetUserByEmail(login.Email);
            
            if (user != null && user.Password == login.Password)
            {
                return true; // O email e a senha foram validados
            }
            else
            {
                Console.WriteLine("Senha errada!");
            }

            return false; // O email e/ou a senha não foram validados
        }


        private string GenerateJwtToken(string email)
        {
            var user = _userRepository.GetUserByEmail(email).Result; // Obter o usuário com base no email

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            if (user != null && user.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name)); // Adicionar a reivindicação de função (role) ao token
            }

            string? secretKey = _configuration["JwtSettings:SecretKey"];
            if (secretKey != null)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:TokenExpirationTimeInMinutes"]));

                var token = new JwtSecurityToken(
                    _configuration["JwtSettings:Issuer"],
                    _configuration["JwtSettings:Audience"],
                    claims,
                    expires: expires,
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new Exception("A chave secreta não está configurada.");
            }
        }

    }
}
