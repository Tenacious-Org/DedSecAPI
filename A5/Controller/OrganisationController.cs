using Microsoft.AspNetCore.Mvc;
using A5.Models;
using A5.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace A5.Controller
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private  readonly ILogger<IOrganisationService> _logger;
        private  readonly IOrganisationService _organisationService;
        public OrganisationController(ILogger<IOrganisationService> logger,  IOrganisationService organisationService)
        {
            _logger = logger;
            _organisationService = organisationService;
        } 

        /// <summary>
        ///  This Method is used to view all organisation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewOrganisation
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param>String</param>
        /// <returns>
        ///Return List of Organisation.
        /// </returns>

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public ActionResult GetAllOrganisation()
        {
            try{
                var data = _organisationService.GetAllOrganisation();
                return Ok(data);
            }
            catch(Exception exception)
            {
                 _logger.LogError("OrganisationController : GetAllOrganisation() : (Error:{Message})",exception.Message);
                return Problem(exception.Message);
            }
            
        }
        
        /// <summary>
        ///  This Method is used to view single Organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleOrganisation
        ///     {
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Returns signle organisation by id
        /// </returns>

         [HttpGet("GetById")]
        public ActionResult GetByOrganisationId( int id)
        {
           if(id<=0) return BadRequest("Organisation Id must be greater than zero");
            try{
               
                var data = _organisationService.GetOrganisationById(id);
                 return Ok(data);
            }
            catch(ValidationException exception)
            {
                _logger.LogError("OrganisationController : GetByOrganisationId(int id) : (Error : {Message})",exception.Message);
                return BadRequest(_organisationService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                _logger.LogError("OrganisationController : GetByOrganisationId(int id) : (Error : {Message})",exception.Message);
                return Problem(exception.Message);
            }
            
        }
        
        /// <summary>
        ///  This Method is used to create new organisation
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateOrganisation
        ///     {
        ///        "OrganisationName" = "Development",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="organisation">String</param>
        /// <returns>
        ///Return true when the Organisation is added in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPost("Create")]
        public ActionResult Create(Organisation organisation)
        {
            if(organisation==null) return BadRequest("Organisation should not be null");
            try
            {   
                organisation.AddedBy=GetCurrentUserId();
                var data=_organisationService.CreateOrganisation(organisation);
                return  Ok(data); 
            }
            catch(ValidationException exception)
            {
                _logger.LogError("OrganisationController : Create(Organisation organisation) : (Error:{Message}",exception.Message);
                return BadRequest(_organisationService.ErrorMessage($"{exception.Message}"));
            }
            catch(Exception exception)
            {
                _logger.LogError("OrganisationController : Create(Organisation organisation) : (Error:{Message}",exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to update single Organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET / ViewSingleOrganisation
        ///     {
        ///        "OrganisationId" = "1",
        ///        "OrganisationName" = "Software Development"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="organisation">String</param>
        /// <returns>
        ///Returns the updated name of signle organisation by id
        /// </returns>

        [HttpPut("Update")]
        public ActionResult Update(Organisation organisation)
        {
           if(organisation==null) return BadRequest("Organisation should not be null");
            try{
                organisation.UpdatedBy=GetCurrentUserId();
                var data=_organisationService.UpdateOrganisation(organisation);
                return Ok(data); 
            }        
            catch(ValidationException exception)
            {
                  _logger.LogError("OrganisationController : Update(Organisation organisation,int id) : (Error: {Message})",exception.Message);
                return BadRequest(_organisationService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                 _logger.LogError("OrganisationController : Update(Organisation organisation,int id) : (Error: {Message})",exception.Message);
                return Problem(exception.Message);
            }
        }

        /// <summary>
        ///  This Method is used to disable the organisation by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / DisableOrganisation
        ///     {
        ///        "OrganisationId" = "1",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <param name="id">String</param>
        /// <returns>
        ///Return true message when the isactive filed is set to 0 in the database otherwise return "Sorry internal error occured".
        /// </returns>

        [HttpPut("Disable")]
        public ActionResult Disable(int id)
        {
            if (id <= 0) return BadRequest("Id must be greater than zero ");   
            try
            {
                var checkEmployee=_organisationService.GetCount(id);
                if(checkEmployee > 0)
                {             
                    return Ok(checkEmployee);
                   
                }
                else
                {
                    var data = _organisationService.DisableOrganisation(id,GetCurrentUserId());
                    return Ok(data);
                }
            }
            catch(ValidationException exception)
            {
                _logger.LogError("OrganisationController : Disable(int id) : (Error : {Message})",exception.Message);
                return BadRequest(_organisationService.ErrorMessage(exception.Message));
            }
            catch(Exception exception)
            {
                 _logger.LogError("OrganisationController : Disable(int id) : (Error : {Message})",exception.Message);
                return BadRequest(exception.Message);
            }
        }
        private int GetCurrentUserId()
        {
            try
            {
                return Convert.ToInt32(User.FindFirst("UserId")?.Value);
            }
            catch (Exception)
            {
                throw new Exception("Error occured while getting current userId");
            }

        }
    }
}