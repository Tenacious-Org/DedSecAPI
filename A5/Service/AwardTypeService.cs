
using System.ComponentModel.DataAnnotations;
using A5.Data;
using A5.Data.Repository.Interface;
using A5.Data.Repository;
using A5.Models;
using A5.Service.Interfaces;
using A5.Data.Validations;

namespace A5.Service
{
    public class AwardTypeService : IAwardTypeService
    {
        private readonly IAwardTypeRepository _awardTypeRepository;
        private readonly ILogger<AwardTypeService> _logger;
        private readonly AwardTypeValidations _awardTypeValidations;
        public AwardTypeService(IAwardTypeRepository awardTypeRepository, ILogger<AwardTypeService> logger, AwardTypeValidations awardTypeValidations)
        {
            _awardTypeRepository = awardTypeRepository;
            _logger = logger;
            _awardTypeValidations = awardTypeValidations;
        }


        //creates awardtype using awardtype object
        public bool CreateAwardType(AwardType awardType)
        {
            if (awardType == null) throw new ValidationException("AwardType should not be null");
            _awardTypeValidations.CreateValidation(awardType);
            try
            {
                awardType.Image = System.Convert.FromBase64String(awardType.ImageString!);
                return _awardTypeRepository.CreateAwardType(awardType);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeService: CreateAwardType(AwardType awardType) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeService: CreateAwardType(AwardType awardType) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //updates awardtype using awardtype object
        public bool UpdateAwardType(AwardType awardType)
        {
            if (awardType == null) throw new ValidationException("AwardType should not be null");
            _awardTypeValidations.UpdateValidation(awardType);
            try
            {
                awardType.Image = System.Convert.FromBase64String(awardType.ImageString!);
                return _awardTypeRepository.UpdateAwardType(awardType);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeService: UpdateAwardType(AwardType awardType) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeService: UpdateAwardType(AwardType awardType) : (Error:{Message}", exception.Message);
                throw;
            }
        }

        //disables awardtype using awardtype id and current user id
        public bool DisableAwardType(int awardTypeId, int userId)
        {
            if (userId <= 0) throw new ValidationException("User id must be greater than Zero");
            if (awardTypeId <= 0) throw new ValidationException("Award type id  must be greater than Zero");
            _awardTypeValidations.DisableValidation(userId);
            try
            {
                return _awardTypeRepository.DisableAwardType(awardTypeId, userId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeService: DisableAwardType(awardTypeId : {awardTypeId},employeeId : {employeeId}) : (Error:{Message}", awardTypeId, userId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeService: DisableAwardType(awardTypeId : {awardTypeId},employeeId : {employeeId}) : (Error:{Message}", awardTypeId, userId, exception.Message);
                throw;
            }
        }

        //gets list of all awardtypes
        public IEnumerable<AwardType> GetAllAwardType()
        {

            try
            {
                return _awardTypeRepository.GetAllAwardTypes();
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeRepository: GetAllAwardType() : (Error:{Message}", exception.Message);
                throw;
            }
        }


        //gets awardtype by awardtype Id
        public AwardType? GetAwardTypeById(int awardTypeId)
        {
            if (awardTypeId <= 0) throw new ValidationException("Award Id must be greater than 0.");
            try
            {
                return _awardTypeRepository.GetAwardTypeById(awardTypeId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardTypeService: GetAwardTypeById(id : {id}) : (Error:{Message}", awardTypeId, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardTypeService: GetAwardTypeById(id : {id}) : (Error:{Message}", awardTypeId, exception.Message);
                throw;
            }
        }
        public object ErrorMessage(string ValidationMessage)
        {
            return new { message = ValidationMessage };
        }

    }
}