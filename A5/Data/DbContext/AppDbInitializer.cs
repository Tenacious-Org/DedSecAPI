using A5.Models;
using Microsoft.AspNetCore.Identity;

namespace A5.Data
{
    public static class AppDbInitializer
    {
         public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>()!;
                context.Database.EnsureCreated();
                if(!context.Organisations?.Any()==true)
                {
                    context?.Organisations?.AddRange(new List<Organisation>() {
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
                    context?.SaveChanges();
                }

                if(!context?.Departments?.Any()==true)
                {
                    context?.Departments?.AddRange(new List<Department>() {
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
                        new Department()
                        {
                            DepartmentName = "LAMP",
                            OrganisationId = 2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        },
                        new Department()
                        {
                            DepartmentName = "Black Box Testing",
                            OrganisationId = 3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        },
                        new Department()
                        {
                            DepartmentName = "White Box Testing",
                            OrganisationId = 3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        },
                        new Department()
                        {
                            DepartmentName = "End to End Testing",
                            OrganisationId = 3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now
                        }
                    });
                    context?.SaveChanges();
                }

                if(!context?.Roles?.Any()==true)
                {
                    context?.Roles?.AddRange(new List<Role>() {
                        new Role()
                        {
                            RoleName = "Public",
                        },
                        new Role()
                        {
                            RoleName = "Requester",
                        },
                        new Role()
                        {
                            RoleName = "Approver",
                        },
                        new Role()
                        {
                            RoleName = "Publisher",
                        },
                         new Role()
                        {
                            RoleName = "Admin",
                        },
                    });
                    context?.SaveChanges();
                }

                 if(!context?.Designations?.Any()==true)
                {
                    context?.Designations?.AddRange(new List<Designation>() {
                        new Designation()
                        {
                            DesignationName = "Admin",
                            DepartmentId = 1,
                            RoleId=5,
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
                            RoleId=5,
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
                            RoleId=4,
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
                            RoleId=4,
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
                            RoleId=3,
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
                            RoleId=2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Trainee",
                            DepartmentId = 2,
                            RoleId=1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "HR",
                            DepartmentId = 3,
                            RoleId=4,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Project Manager",
                            DepartmentId = 3,
                            RoleId=3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Team Leader",
                            DepartmentId = 3,
                            RoleId=2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Trainee",
                            DepartmentId = 3,
                            RoleId=1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "HR",
                            DepartmentId = 4,
                            RoleId=4,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Project Manager",
                            DepartmentId = 4,
                            RoleId=3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Team Leader",
                            DepartmentId = 4,
                            RoleId=2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Trainee",
                            DepartmentId = 4,
                            RoleId=1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "HR",
                            DepartmentId = 5,
                            RoleId=4,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Project Manager",
                            DepartmentId = 5,
                            RoleId=3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Team Leader",
                            DepartmentId = 5,
                            RoleId=2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Trainee",
                            DepartmentId = 5,
                            RoleId=1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "HR",
                            DepartmentId = 6,
                            RoleId=4,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Project Manager",
                            DepartmentId = 6,
                            RoleId=3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Team Leader",
                            DepartmentId = 6,
                            RoleId=2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Trainee",
                            DepartmentId = 6,
                            RoleId=1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "HR",
                            DepartmentId = 7,
                            RoleId=4,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Project Manager",
                            DepartmentId = 7,
                            RoleId=3,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Team Leader",
                            DepartmentId = 7,
                            RoleId=2,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        },
                        new Designation()
                        {
                            DesignationName = "Trainee",
                            DepartmentId = 7,
                            RoleId=1,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now

                        }
                    });
                    context?.SaveChanges();
                }
                
                if (!context?.Employees?.Any()==true)
                {
                    context?.Employees?.AddRange(new List<Employee>() { 
                       new Employee()
                       {
                           ACEID = "ACE001",
                           FirstName = "Admin",
                           LastName = "Tenacious",
                           Email = "admin@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
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
                           FirstName = "Gregory",
                           LastName = "Howard",
                           Email = "ajay@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-18000),
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
                           FirstName = "Peter",
                           LastName = "Parker",
                           Email = "jeeva@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 1,
                           DepartmentId = 1,
                           DesignationId = 3,
                           ReportingPersonId = 2,
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
                           ACEID = "ACE004",
                           FirstName = "Colten",
                           LastName = "Sutton",
                           Email = "jith@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-11000),
                           OrganisationId = 2,
                           DepartmentId = 2,
                           DesignationId = 4,
                           ReportingPersonId = 3,
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
                           ACEID = "ACE005",
                           FirstName = "William",
                           LastName = "Butcher",
                           Email = "aakaash@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
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
                           FirstName = "Lila",
                           LastName = "Snyder",
                           Email = "priya@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Female",
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
                           FirstName = "Sage",
                           LastName = "Garrett",
                           Email = "atsaya@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Female",
                           DOB = DateTime.Now.AddDays(-13600),
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
                       new Employee()
                       {
                           ACEID = "ACE008",
                           FirstName = "Helena",
                           LastName = "Sosa",
                           Email = "archana@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Female",
                           DOB = DateTime.Now.AddDays(-13000),
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
                       new Employee()
                       {
                           ACEID = "ACE009",
                           FirstName = "Jocelyn",
                           LastName = "Dickson",
                           Email = "karthi@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
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
                       new Employee()
                       {
                           ACEID = "ACE010",
                           FirstName = "Charity",
                           LastName = "Vaughan",
                           Email = "loki@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-14700),
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
                       new Employee()
                       {
                           ACEID = "ACE011",
                           FirstName = "Arnav",
                           LastName = "Rush",
                           Email = "rush@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-14700),
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
                       new Employee()
                       {
                           ACEID = "ACE6010",
                           FirstName = "Mitchell",
                           LastName = "Austin",
                           Email = "mitch@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 3,
                           DesignationId = 8,
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
                           ACEID = "ACE6011",
                           FirstName = "Easton",
                           LastName = "Hodges",
                           Email = "hodges@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 3,
                           DesignationId = 9,
                           ReportingPersonId = 12,
                           HRId = 12,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6012",
                           FirstName = "Russell",
                           LastName = "Burns",
                           Email = "burns@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 3,
                           DesignationId = 10,
                           ReportingPersonId = 13,
                           HRId = 12,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6013",
                           FirstName = "Brycen",
                           LastName = "Evans",
                           Email = "evans@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 3,
                           DesignationId = 11,
                           ReportingPersonId = 14,
                           HRId = 12,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6014",
                           FirstName = "Damion",
                           LastName = "Oconnor",
                           Email = "oconnor@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 4,
                           DesignationId = 12,
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
                           ACEID = "ACE6015",
                           FirstName = "Lilian",
                           LastName = "Blake",
                           Email = "blake@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 4,
                           DesignationId = 13,
                           ReportingPersonId = 16,
                           HRId = 16,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6016",
                           FirstName = "Reece",
                           LastName = "Logan",
                           Email = "logan@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 4,
                           DesignationId = 14,
                           ReportingPersonId = 17,
                           HRId = 16,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6017",
                           FirstName = "Catherine",
                           LastName = "Bridges",
                           Email = "bridges@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Female",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 4,
                           DesignationId = 15,
                           ReportingPersonId = 18,
                           HRId = 16,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6018",
                           FirstName = "Dustin",
                           LastName = "Molly",
                           Email = "molly@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 16,
                           ReportingPersonId = 3,
                           HRId = 20,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6019",
                           FirstName = "Byron",
                           LastName = "Lester",
                           Email = "lester@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 17,
                           ReportingPersonId = 20,
                           HRId = 20,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6020",
                           FirstName = "Alexandria",
                           LastName = "Maddox",
                           Email = "maddox@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Female",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 18,
                           ReportingPersonId = 21,
                           HRId = 20,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6021",
                           FirstName = "Simon",
                           LastName = "Wang",
                           Email = "wang@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 19,
                           ReportingPersonId = 22,
                           HRId = 20,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6022",
                           FirstName = "Justin",
                           LastName = "Frederick",
                           Email = "frederick@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 20,
                           ReportingPersonId = 3,
                           HRId = 24,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6023",
                           FirstName = "Rhianna ",
                           LastName = "Watson",
                           Email = "watson@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Female",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 21,
                           ReportingPersonId = 24,
                           HRId = 24,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6024",
                           FirstName = "Yaretzi",
                           LastName = "Gibbs",
                           Email = "gibbs@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 22,
                           ReportingPersonId = 25,
                           HRId = 24,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6025",
                           FirstName = "John",
                           LastName = "Weiss",
                           Email = "weiss@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 23,
                           ReportingPersonId = 26,
                           HRId = 24,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6026",
                           FirstName = "Willie",
                           LastName = "Macdonald",
                           Email = "macd@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 24,
                           ReportingPersonId = 3,
                           HRId = 28,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6031",
                           FirstName = "Chase",
                           LastName = "Buck",
                           Email = "buck@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 25,
                           ReportingPersonId = 28,
                           HRId = 28,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                       new Employee()
                       {
                           ACEID = "ACE6028",
                           FirstName = "Kamora",
                           LastName = "Thanos",
                           Email = "thanos@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Female",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 26,
                           ReportingPersonId = 29,
                           HRId = 28,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },
                        new Employee()
                       {
                           ACEID = "ACE6029",
                           FirstName = "Xavier",
                           LastName = "Cummings",
                           Email = "xavier@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 27,
                           ReportingPersonId = 30,
                           HRId = 28,
                           Password = "Admin@123",
                           IsActive = true,
                           AddedBy = 1,
                           AddedOn = DateTime.Now,
                           UpdatedBy = 1,
                           UpdatedOn = DateTime.Now
                       },


                    });
                    context?.SaveChanges();
                }
                if(!context?.AwardTypes?.Any()==true)
                {
                    context?.AwardTypes?.AddRange(new List<AwardType>() {
                        new AwardType()
                        {
                            AwardName = "Role Star",
                            AwardDescription = "Best Performer.",
                            Image = null,
                            ImageName=null,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedOn = null
                        },
                        new AwardType()
                        {
                            AwardName = "Gladiator",
                            AwardDescription = "Hard Worker.",                            
                            Image = null,
                            ImageName=null,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedOn = null
                        },
                        new AwardType()
                        {
                            AwardName = "First Victor",
                            AwardDescription = "Inpirationsal Acheiver.",
                            Image = null,
                            ImageName=null,
                            IsActive = true,
                            AddedBy = 1,
                            AddedOn = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedOn = null
                        },
                    });
                    context?.SaveChanges();
                }
                if(!context?.Statuses?.Any()==true)
                {
                    context?.Statuses?.AddRange(new List<Status>() {
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
                    context?.SaveChanges();
                }

                

                if(!context?.Awards?.Any()==true)
                {
                    context?.Awards?.AddRange(new List<Award>() {
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
                         new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 8,
                            RequesterId = 6,
                            ApproverId = 5,
                            HRId = 4,
                            StatusId = 2,
                            Reason = "Hard Worker really a Team man.",
                            RejectedReason = null,
                            CouponCode = null,
                            AddedBy = 6,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 4,
                            UpdatedOn = DateTime.Now
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 9,
                            RequesterId = 6,
                            ApproverId = 5,
                            HRId = 4,
                            StatusId = 3,
                            Reason = "Happy to have him in our team, Brilliant performer.",
                            RejectedReason = null,
                            CouponCode = null,
                            AddedBy = 6,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 4,
                            UpdatedOn = DateTime.Now
                        },
                        new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 10,
                            RequesterId = 6,
                            ApproverId = 5,
                            HRId = 4,
                            StatusId = 1,
                            Reason = "Smart worker, Deserved every applause for the work he had put on.",
                            RejectedReason = null,
                            CouponCode = null,
                            AddedBy = 6,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 4,
                            UpdatedOn = DateTime.Now
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Hard worker, really a well deserved person for this award",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-14)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-35)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-13)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-13)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-25)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-33)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-22)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-40)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-7)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-21)
                        },new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-12)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-13)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 15,
                            RequesterId = 14,
                            ApproverId = 13,
                            HRId = 12,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH1276HBH",
                            AddedBy = 14,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 12,
                            UpdatedOn = DateTime.Now.AddDays(-34)
                        },



                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                                                ////////////////////////////////////////////////////////////////////////////////////////////////////
                                                                        ////////////////////////////////////////////////////////////////////////////////////////////////////
                                                                                                ////////////////////////////////////////////////////////////////////////////////////////////////////



                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 8,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-18)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-18)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-18)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-18)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-18)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 19,
                            RequesterId = 18,
                            ApproverId = 17,
                            HRId = 16,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 18,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 16,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-22)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-21)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-22)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-21)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-22)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-21)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-22)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 23,
                            RequesterId = 22,
                            ApproverId = 21,
                            HRId = 20,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 22,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 20,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-26)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-26)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-26)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-26)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-26)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 27,
                            RequesterId = 26,
                            ApproverId = 25,
                            HRId = 24,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 26,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 24,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },
                        new Award()
                        {
                            AwardTypeId = 1,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                        new Award()
                        {
                            AwardTypeId = 2,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-30)
                        },
                         new Award()
                        {
                            AwardTypeId = 3,
                            AwardeeId = 31,
                            RequesterId = 30,
                            ApproverId = 29,
                            HRId = 28,
                            StatusId = 4,
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = "KJ7JH876HBH",
                            AddedBy = 30,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 28,
                            UpdatedOn = DateTime.Now.AddDays(-17)
                        },

                    });
                    context?.SaveChanges();
                }
               
            }
        }
        
       
    }
}