using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using A5.Service;
using A5.Service.Interfaces;

using A5.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace A5.Service
{
   
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly EmployeeService _employeeService;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IConfiguration configuration, EmployeeService employeeService, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _employeeService = employeeService;
            _logger = logger;

        }

        public object GenerateToken(Login Credentials)
        {
            var user = _employeeService.GetEmployee(Credentials!.Email!, Credentials!.Password!);
            if (user == null) throw new ValidationException("User should not be null");
            try
            {
                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email,user.Email!),
                        new Claim("UserId",user.Id.ToString()),
                        new Claim(ClaimTypes.Role,user.Designation!.RoleId.ToString()),
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                        claims,
                    expires: DateTime.UtcNow.AddMinutes(360),
                    signingCredentials: signIn);

                var Result = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiryInMinutes = 360,
                    UserId=user.Id,
                    IsRole = user.Designation.RoleId,
                    IsRequester = user.Designation.RoleId == 2 ? true : false,
                    IsApprover = user.Designation.RoleId == 3? true : false,
                    IsPublisher = user.Designation.RoleId == 4 ? true : false,
                    IsAdmin = user.Designation.RoleId == 5? true : false,
                };

                return Result;

            }
            catch(ValidationException exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                _logger.LogInformation("Token Service: GenerateToken(Login Credentials) : (Error:{Message}",exception.Message);
                throw;
            }
            catch(Exception exception)
            {
                _logger.LogError("Error: {Message}",exception.Message);
                throw;
            }
        }
    }
}