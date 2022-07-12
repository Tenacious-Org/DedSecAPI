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
        public TokenController(TokenService tokenService)
        {

            _tokenService = tokenService;
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