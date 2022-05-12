using A5.Models;

namespace A5.Data
{
    public class AppDbInitializer
    {
         public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                

                if(!context.Organisations.Any())
                {
                    context.Organisations.AddRange(new List<Organisation>() {
                        new Organisation()
                        {
                            OrganisationName = "Tenacious",
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        },
                        new Organisation()
                        {
                            OrganisationName = "Development",
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        },
                        new Organisation()
                        {
                            OrganisationName = "Testing",
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        },
                    });
                    context.SaveChanges();
                }
                if(!context.Departments.Any())
                {
                    context.Departments.AddRange(new List<Department>() {
                        new Department()
                        {
                            DepartmentName = "Management",
                            OrganisationId = 1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Department()
                        {
                            DepartmentName = "Dotnet",
                            OrganisationId = 2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Department()
                        {
                            DepartmentName = "JAVA",
                            OrganisationId = 2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        },
                    });
                    context.SaveChanges();
                }
                if(!context.Designations.Any())
                {
                    context.Designations.AddRange(new List<Designation>() {
                        new Designation()
                        {
                            DesignationName = "Admin",
                            DepartmentId = 1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "CEO",
                            DepartmentId = 1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "VP",
                            DepartmentId = 1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "HR",
                            DepartmentId = 2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Project Manager",
                            DepartmentId = 2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Team Leader",
                            DepartmentId = 2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>() { 
                       new Employee()
                       {
                           ACEID = "ACE001",
                           FirstName = "Admin",
                           LastName = "Tenacious",
                           Email = "admin@tenacious.com",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 1,
                           DepartmentId = 1,
                           DesignationId = 1,
                           ReportingPersonId = null,
                           HRId = null,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE002",
                           FirstName = "Ajay",
                           LastName = "Bharathi",
                           Email = "ajay@tenacious.com",
                           DOB = DateTime.Now.AddDays(-10000),
                           OrganisationId = 1,
                           DepartmentId = 1,
                           DesignationId = 2,
                           ReportingPersonId = null,
                           HRId = null,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE003",
                           FirstName = "Jeevanantham",
                           LastName = "N",
                           Email = "jeeva@tenacious.com",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 1,
                           DepartmentId = 1,
                           DesignationId = 3,
                           ReportingPersonId = 2,
                           HRId = null,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE004",
                           FirstName = "Madhu",
                           LastName = "Jith",
                           Email = "jith@tenacious.com",
                           DOB = DateTime.Now.AddDays(-11000),
                           OrganisationId = 2,
                           DepartmentId = 2,
                           DesignationId = 4,
                           ReportingPersonId = 3,
                           HRId = 2,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE005",
                           FirstName = "Aakash",
                           LastName = "Aakaash",
                           Email = "aakaash@tenacious.com",
                           DOB = DateTime.Now.AddDays(-13000),
                           OrganisationId = 2,
                           DepartmentId = 2,
                           DesignationId = 5,
                           ReportingPersonId = 4,
                           HRId = 4,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE006",
                           FirstName = "Vidhya",
                           LastName = "Priya",
                           Email = "priya@tenacious.com",
                           DOB = DateTime.Now.AddDays(-15000),
                           OrganisationId = 2,
                           DepartmentId = 2,
                           DesignationId = 6,
                           ReportingPersonId = 5,
                           HRId = 4,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE007",
                           FirstName = "Atsaya",
                           LastName = "A",
                           Email = "atsaya@tenacious.com",
                           DOB = DateTime.Now.AddDays(-14000),
                           OrganisationId = 2,
                           DepartmentId = 2,
                           DesignationId = 7,
                           ReportingPersonId = 6,
                           HRId = 4,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },

                    });
                    context.SaveChanges();
                }
                if(!context.AwardTypes.Any())
                {
                    context.AwardTypes.AddRange(new List<AwardType>() {
                        new AwardType()
                        {
                            AwardName = "Role Star",
                            AwardDescription = "Best Performer.",
                            IsActive = true,
                            Image = null,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedOn = null
                        },
                        new AwardType()
                        {
                            AwardName = "Gladiator",
                            AwardDescription = "Hard Worker.",
                            IsActive = true,
                            Image = null,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedOn = null
                        },
                        new AwardType()
                        {
                            AwardName = "First Victor",
                            AwardDescription = "Inpirationsal Acheiver.",
                            IsActive = true,
                            Image = null,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedOn = null
                        },
                    });
                    context.SaveChanges();
                }
                if(!context.Statuses.Any())
                {
                    context.Statuses.AddRange(new List<Status>() {
                        new Status()
                        {
                            StatusName = "Pending",
                            IsActive = true
                        },
                        new Status()
                        {
                            StatusName = "Approved",
                            IsActive = true
                        },
                        new Status()
                        {
                            StatusName = "Rejected",
                            IsActive = true
                        },
                        new Status()
                        {
                            StatusName = "Published",
                            IsActive = true
                        },
                    });
                    context.SaveChanges();
                }
                if(!context.Awards.Any())
                {
                    context.Awards.AddRange(new List<Award>() {
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 7,
                            RequesterId = 6,
                            ApproverId = 5,
                            HRId = 4,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 6,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 4,
                            UpdatedOn = DateTime.Now
                        },
                        
                    });
                    context.SaveChanges();
                }

        }
    }
}
}