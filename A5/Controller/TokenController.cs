using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service;
using System.ComponentModel.DataAnnotations;
using A5.Service.Interfaces;

namespace A5.Controller
{
    [ApiController]
    [Route("[controller]")]

    public class TokenController : ControllerBase
    {

        private readonly TokenService _tokenService;
        private readonly ILogger<TokenController> _logger;
        public TokenController(TokenService tokenService,ILogger<TokenController> logger)
        {

            _tokenService = tokenService;
            _logger=logger;
        }

        /// <summary>
        ///  This Method is used to Generate Authtoken.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / AuthToken
        ///     {
        ///        "email": "admin@tenacious.com",
        ///        "password": "Admin@123"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param >String</param>
        /// <returns>
        ///Returns token to authorize the Swagger to make run all methods 
        /// </returns>


        [HttpPost]
        public IActionResult AuthToken(Login Crendentials)
        {
          if ( Crendentials == null ) return BadRequest("Login Credentials cannot be null");

          try
            {              
                var Result = _tokenService.GenerateToken(Crendentials);
                return Ok(Result);

            }
             catch(ValidationException exception)
            {
                 _logger.LogError("TokenController :AuthToken(Login Credentials) : (Error: {Message})",exception.Message);
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                 _logger.LogError("TokenController :AuthToken(Login Credentials) : (Error: {Message})",exception.Message);
                return Problem(exception.Message);
            }

        }


    }


}