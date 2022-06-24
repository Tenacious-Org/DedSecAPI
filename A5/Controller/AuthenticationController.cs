using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using A5.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace A5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;  
        private readonly IConfiguration _configuration;  
  
        public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)  
        {  
            userManager = userManager;  
            _configuration = configuration;
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
                    expiration = token.ValidTo  
                });  
            }  
            return Unauthorized();  
        }  


        //Ithuku mela irukurathu ok dhaan athula no changes kila irukuthula mattum Employee controller la varum maari pannu macha.
        // Error Adikum en endraal register model nu onnu kaedayaathu ignore.

        [HttpPost]  
        [Route("register-user")]  
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)  
        {  
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
            
            if (await roleManager.RoleExistsAsync(UserRoles.User))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.User);  
            }
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }  
  
        [HttpPost]  
        [Route("register-admin")]  
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)  
        {  
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
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
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }

        [HttpPost]  
        [Route("register-requester")]  
        public async Task<IActionResult> RegisterRequester([FromBody] RegisterModel model)  
        {  
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });   
            
            if (!await roleManager.RoleExistsAsync(UserRoles.Requester))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Requester));  
  
            if (await roleManager.RoleExistsAsync(UserRoles.Requester))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.Requester);  
            }  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }

        [HttpPost]  
        [Route("register-approver")]  
        public async Task<IActionResult> RegisterApprover([FromBody] RegisterModel model)  
        {  
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });   

            if (!await roleManager.RoleExistsAsync(UserRoles.Approver))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Approver));  

            if (await roleManager.RoleExistsAsync(UserRoles.Approver))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.Approver);  
            }  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }

        [HttpPost]  
        [Route("register-publisher")]  
        public async Task<IActionResult> RegisterPublisher([FromBody] RegisterModel model)  
        {  
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });   
            
            if (!await roleManager.RoleExistsAsync(UserRoles.Publisher))  
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Publisher));  
  
            if (await roleManager.RoleExistsAsync(UserRoles.Publisher))  
            {  
                await userManager.AddToRoleAsync(user, UserRoles.Publisher);  
            }  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }
    }
  
    }
    
}