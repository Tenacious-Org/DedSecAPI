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

        private TokenService _tokenService;
        private ILogger<TokenController> _logger;
        public TokenController(TokenService tokenService, ILogger<TokenController> logger)
        {

            _tokenService = tokenService;
            _logger = logger;
        }

       

        [HttpPost]
        public IActionResult AuthToken(Login Crendentials)
        {
          try
            {
                if ( Crendentials == null ) return BadRequest("Login Credentials cannot be null");
                var Result = _tokenService.GenerateToken(Crendentials);
                return Ok(Result);

            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }


    }


}