using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using A5.Models;
using Microsoft.EntityFrameworkCore;
using A5.Service.Interfaces;
using A5.Data.Repository.Interface;
using A5.Data.Validations;
using A5.Service;
using A5.Utilities;

namespace A5.Data.Repository
{
    public class AwardRepository : IAwardRepository
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<IAwardService> _logger;
        private readonly MailService _mail;

        public AwardRepository(AppDbContext context, IEmployeeRepository employeeRepository, ILogger<IAwardService> logger, MailService mail)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _logger = logger;
            _mail = mail;

        }
        
        //raises award request by using award object and employee
        public bool RaiseAwardRequest(Award award, int employeeId)
        {
            AwardValidations.RequestValidation(award);
            try
            {
                var employee = _employeeRepository.GetEmployeeById(employeeId);
                if (employee == null) throw new ValidationException("Requester Details Not Found"); //
                _context.Set<Award>().Add(award);
                var aid = award.AwardeeId;
                award.RequesterId = employee!.Id;
                award.ApproverId = employee.ReportingPersonId;
                award.HRId = GetHRID(aid);
                award.StatusId = 1;
                award.AddedBy = employeeId;
                award.AddedOn = DateTime.Now;
                _context.SaveChanges();
                if (award.StatusId == 1)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.RequesterAsync(awardee);
                }
                return true;

            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : RaiseAwardRequest(Award award,int employeeId) : (Error:{Message}", exception.Message);
                return false;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : RaiseAwardRequest(Award award,int employeeId) : (Error:{Message}", exception.Message);
                return false;
            }
        }
        
        //Gets HR id by using awardee id
        public int? GetHRID(int awardeeId)
        {
            var list = _context.Set<Employee>().FirstOrDefault(nameof => nameof.Id == awardeeId)!.HRId;
            
            return list; 
        } //
        
        //approves the request raised by using award object
        public bool ApproveRequest(Award award)
        {
            AwardValidations.ApprovalValidation(award);
            try
            {
                if (award.StatusId == 4)
                {
                    bool IsIdAlreadyExists = _context.Awards!.Any(nameof => nameof.CouponCode == award.CouponCode);
                    if (IsIdAlreadyExists) throw new ValidationException("CouponCode already redeemed");
                }
                _context.Set<Award>().Update(award);
                award.UpdatedOn = DateTime.Now;
                _context.SaveChanges();
                if (award.StatusId == 4)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.ExampleAsync(awardee);

                }
                else if (award.StatusId == 3)
                {
                    var awardee = GetAwardById(award.Id);
                    _mail?.RejectedAsync(awardee!);
                }
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : ApproveRequest(Award award) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                
                _logger.LogError("AwardRepository : ApproveRequest(Award award) : (Error:{Message}", exception.Message);
                return false;
            }
        }

        //Gets award by using award id
        public Award? GetAwardById(int awardId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .FirstOrDefault(nameof => nameof.Id == awardId);
                return award != null ? award : throw new ValidationException($"There is no matching records found for award Id -{awardId} , enter the correct award Id");
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAwardById(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardRepository : GetAwardById(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //Adds the comment by using comment object and employee id
        public bool AddComments(Comment comment, int employeeId)
        {
            AwardValidations.ValidateAddComment(comment);
            try
            {
                _context.Set<Comment>().Add(comment);
                comment.EmployeeId = employeeId;
                comment.CommentedOn = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : AddComments(Comment comment, int employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : AddComments(Comment comment, int employeeId) : (Error:{Message}", exception.Message);
                return false;
            }
        }

        //returns the comments by using award id
        public IEnumerable<Comment> GetComments(int awardId)
        {
            AwardValidations.ValidateGetComments(awardId);
            try
            {
                var comments = _context.Set<Comment>()
                     .Include("Employees")
                     .Include("Awards")
                     .Where(nameof => nameof.AwardId == awardId)
                     .ToList();
                return comments;
            }
            catch (ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetComments(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetComments(int awardId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //returns list of all awards by using page id and employee id
        public IEnumerable<Award> GetAllAwardsList(int? pageId, int? employeeId)
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .ToList();
                if (pageId == 0 || employeeId == 0)
                    award = award.Where(nameof => nameof.StatusId == 4).ToList();
                else if (pageId == 1 && employeeId != 0)
                    award = award.Where(nameof => nameof.StatusId == 4 && nameof.AwardeeId == employeeId).ToList();
                else if (pageId == 2 && employeeId != 0)
                    award = award.Where(nameof => nameof.RequesterId == employeeId).OrderBy(nameof => nameof.StatusId ).OrderByDescending(nameof=>nameof.AddedOn).ToList();
                else if (pageId == 3 && employeeId != 0)
                    award = award.Where(nameof => nameof.ApproverId == employeeId).OrderBy(nameof => nameof.StatusId).OrderByDescending(nameof=>nameof.AddedOn).ToList();
                else if (pageId == 4 && employeeId != 0)
                    award = award.Where(nameof => nameof.HRId == employeeId && (nameof.StatusId == 2 || nameof.StatusId == 4)).OrderByDescending(nameof => nameof.UpdatedOn).ToList();         
                return award;
            }
            catch (Exception exception)
            {
                _logger.LogError("AwardRepository : GetAllAwardsList(int? pageId, int? employeeId) : (Error:{Message}", exception.Message);
                throw;
            }
        }
        
        //returns the list of all awards
        public IEnumerable<Award> GetAllAwardees()
        {
            try
            {
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status")
                    .Where(nameof => nameof.StatusId == 4)
                    .ToList();


                return award;
            }
            catch (Exception exception)
            {
               _logger.LogError("AwardRepository : GetAllAwardees(): (Error:{Message}", exception.Message);
                throw;
            }
        }

        

        public IEnumerable<Award> GetDashboardDetailsByFilters(int? organisationId, int? departmentId, int? awardId, DateTime? start, DateTime? end)
        {
            try
            {
                var fdate = new DateTime(0001,04,15);
                var tdate = new DateTime(0001,04,29);



                // if(organisationId != 0 && departmentId != 0 && awardId != 0 && start.ToString() != fdate && end.ToString() != tdate)
                // {
                //     Console.WriteLine("Condition Satisfied. All Filters Applied.");
                // }
                // if(organisationId != 0 && departmentId != 0 && awardId != 0 && start.ToString() != fdate && end.ToString() == tdate)
                // {
                //      Console.WriteLine("Condition Satisfied. ODAS Filters Applied.");
                //      var Update = _context.Set<Award>().FirstOrDefault(nameof => nameof.Id == 1)!.UpdatedOn;
                //      Console.WriteLine("2022-07-15 21:59:57.6137714");
                //      Update = Convert.ToDateTime(Update);
                //      Console.WriteLine("Update: ", Update.GetType());
                     
                //      Console.WriteLine("Mass: ",Convert.ToDateTime(start).GetType());
                //      if(Update == start)
                //      {
                //         Console.WriteLine("Satisfied.");
                //      }
                // }

                Console.WriteLine(start == fdate);

                Console.WriteLine("fdate: ", fdate);
                Console.WriteLine("tdate: ", tdate);
                Console.WriteLine("start: ", start.ToString());
                Console.WriteLine("end: ", end.ToString());

                if(organisationId != 0 && departmentId == 0 && awardId == 0 && start == fdate && end == tdate){
                    Console.WriteLine("Satisfied.");
                }
                


            
                var award = _context.Set<Award>()
                    .Include("Awardee")
                    .Include("Awardee.Designation")
                    .Include("Awardee.Designation.Department")
                    .Include("Awardee.Designation.Department.Organisation")
                    .Include("Awardee.ReportingPerson")
                    .Include("Awardee.ReportingPerson.ReportingPerson")
                    .Include("Awardee.HR")
                    .Include("AwardType")
                    .Include("Status") 

                    //Applying All Filter Values and Retrieve the Data Using Organisation ID, Department ID, Award ID, Start Date and End Date  //
                    .WhereIf(organisationId != 0 && departmentId != 0 && awardId != 0 && start != fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id == departmentId 
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= Convert.ToDateTime(start).Date && nameof.UpdatedOn <= Convert.ToDateTime(end).Date))
                    
                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Department ID, Award ID, Start Date   //
                    .WhereIf(organisationId != 0 && departmentId != 0 && awardId != 0 && start != fdate && end == tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id == departmentId 
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= Convert.ToDateTime(start).Date))
                    
                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Department ID, Award ID, End Date     //
                    .WhereIf(organisationId != 0 && departmentId != 0 && awardId != 0 && start == fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id == departmentId 
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn <= Convert.ToDateTime(end).Date)
           
                    //Applying Filter Values and Retrieve the Data Using Start Date  //
                    .WhereIf(organisationId == 0 && departmentId == 0 && awardId == 0 && start != fdate && end == tdate,
                           nameof => nameof.StatusId == 4
                                  && nameof.UpdatedOn >= Convert.ToDateTime(start).Date)
                    
                    
                    //Applying All Filter Values and Retrieve the Data Using End Date   //
                    .WhereIf(organisationId == 0 && departmentId == 0 && awardId == 0 && start == fdate && end != tdate,
                           nameof => nameof.StatusId == 4
                                  && nameof.UpdatedOn <= Convert.ToDateTime(end).Date)
                    
                    //Applying All Filter Values and Retrieve the Data Using Start Date and End Date    //
                    .WhereIf(organisationId == 0 && departmentId == 0 && awardId == 0 && start != fdate && end != tdate,
                           nameof => nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= Convert.ToDateTime(start).Date && nameof.UpdatedOn <= Convert.ToDateTime(end).Date))

                    // Applying All Filter Values and Retrieve the Data Using Organisation ID
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId == 0,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId && nameof.StatusId == 4)
                    
                    //Applying All Filter Values and Retrieve the Data Using Organisation ID, Start Date
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId == 0 && start != fdate && end == tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn >= Convert.ToDateTime(start).Date)
                    
                    //Applying All Filter Values and Retrieve the Data Using Organisation ID, End Date
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId == 0 && start == fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn <= Convert.ToDateTime(end).Date)
                    
                    //Applying All Filter Values and Retrieve the Data Using Organisation ID, Start Date and End Date
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId == 0 && start != fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= Convert.ToDateTime(start).Date && nameof.UpdatedOn <= Convert.ToDateTime(end).Date))
                    
                    //Applying All Filter Values and Retrieve the Data Using Award ID
                    .WhereIf(organisationId == 0 && departmentId == 0 && awardId != 0 && start == fdate && end == tdate,
                           nameof => nameof.AwardTypeId == awardId && nameof.StatusId == 4)
                    
                    //Applying All Filter Values and Retrieve the Data Using Award ID, Start Date
                    .WhereIf(organisationId == 0 && departmentId == 0 && awardId != 0 && start != fdate && end == tdate,
                           nameof => nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn >= Convert.ToDateTime(start).Date)
                    
                    //Applying All Filter Values and Retrieve the Data Using Award ID End Date
                    .WhereIf(organisationId == 0 && departmentId == 0 && awardId != 0 && start == fdate && end != tdate,
                           nameof => nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn <= Convert.ToDateTime(end).Date)
                    
                    //Applying All Filter Values and Retrieve the Data Using Award ID, Start Date and End Date
                    .WhereIf(organisationId == 0 && departmentId == 0 && awardId != 0 && start != fdate && end != tdate,
                           nameof => nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= Convert.ToDateTime(start).Date && nameof.UpdatedOn <= Convert.ToDateTime(end).Date))

                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Department ID.
                    .WhereIf(organisationId != 0 && departmentId != 0 && awardId == 0 && start == fdate && end == tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id == departmentId
                                  && nameof.StatusId == 4)
                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Department ID, Start Date.
                    .WhereIf(organisationId != 0 && departmentId != 0 && awardId == 0 && start != fdate && end == tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id == departmentId   
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn >= Convert.ToDateTime(start).Date)

                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Department ID, End Date
                    .WhereIf(organisationId != 0 && departmentId != 0 && awardId == 0 && start == fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id == departmentId 
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn <= Convert.ToDateTime(end).Date)

                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Department ID, Start Date and End Date
                    .WhereIf(organisationId != 0 && departmentId != 0 && awardId == 0 && start != fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.Awardee.Designation.Department.Id ==departmentId 
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= Convert.ToDateTime(start).Date && nameof.UpdatedOn <= Convert.ToDateTime(end).Date))

                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Award ID
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId != 0 && start == fdate && end == tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4)

                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Award ID, Start Date
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId != 0 && start != fdate && end == tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn >= Convert.ToDateTime(start).Date)

                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Award ID, End Date
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId != 0 && start == fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && nameof.UpdatedOn <= Convert.ToDateTime(end).Date)

                    //Applying Filter Values and Retrieve the Data Using Organisation ID, Award ID, Start Date and End Date
                    .WhereIf(organisationId != 0 && departmentId == 0 && awardId != 0 && start != fdate && end != tdate,
                           nameof => nameof.Awardee!.Designation!.Department!.Organisation!.Id == organisationId
                                  && nameof.AwardTypeId == awardId
                                  && nameof.StatusId == 4
                                  && (nameof.UpdatedOn >= Convert.ToDateTime(start).Date && nameof.UpdatedOn <= Convert.ToDateTime(end).Date))

                    .ToList();
                return award;
            }
             catch(ValidationException exception)
            {
                _logger.LogError("AwardRepository : GetAllbyOrgwise(int id) : (Error:{Message}",exception.Message);
                throw;
            }
            catch (Exception exception){
              _logger.LogError("Error: {Message}",exception.Message);
              throw;
            }
        }
        
       
   }
}