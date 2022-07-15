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
                            DepartmentName = "E2E Testing",
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
                           FirstName = "Ajay",
                           LastName = "Bharathi",
                           Email = "ajay@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
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
                           FirstName = "Madhu",
                           LastName = "Jith",
                           Email = "jith@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
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
                           FirstName = "Aakaash",
                           LastName = "M",
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
                           FirstName = "Vidhya",
                           LastName = "Priya",
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
                           FirstName = "Atsaya",
                           LastName = "A",
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
                           FirstName = "Archana",
                           LastName = "M",
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
                           FirstName = "Karthikraja",
                           LastName = "S",
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
                           FirstName = "Lokesh",
                           LastName = "Kaithi",
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
                           FirstName = "Aravinth",
                           LastName = "S",
                           Email = "aravinth@tenacious.com",
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
                           FirstName = "Rishabh",
                           LastName = "Pant",
                           Email = "pant@tenacious.com",
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
                           FirstName = "Shubman",
                           LastName = "Gill",
                           Email = "shubman@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 3,
                           DesignationId = 9,
                           ReportingPersonId = 8,
                           HRId = 8,
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
                           FirstName = "Virat",
                           LastName = "Kohli",
                           Email = "virat@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 3,
                           DesignationId = 10,
                           ReportingPersonId = 9,
                           HRId = 8,
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
                           FirstName = "Rohit",
                           LastName = "Sharma",
                           Email = "rohit@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 3,
                           DesignationId = 11,
                           ReportingPersonId = 10,
                           HRId = 8,
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
                           FirstName = "Glen",
                           LastName = "Maxwell",
                           Email = "glen@tenacious.com",
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
                           FirstName = "Steven",
                           LastName = "Smith",
                           Email = "steven@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 4,
                           DesignationId = 13,
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
                           ACEID = "ACE6016",
                           FirstName = "Aron",
                           LastName = "Finch",
                           Email = "aron@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 4,
                           DesignationId = 14,
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
                           ACEID = "ACE6017",
                           FirstName = "Josh",
                           LastName = "Hazelwood",
                           Email = "josh@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 2,
                           DepartmentId = 4,
                           DesignationId = 15,
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
                           ACEID = "ACE6018",
                           FirstName = "Ben",
                           LastName = "Stokes",
                           Email = "ben@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 16,
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
                           ACEID = "ACE6019",
                           FirstName = "Joe",
                           LastName = "Root",
                           Email = "joe@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 17,
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
                           ACEID = "ACE6020",
                           FirstName = "Jason",
                           LastName = "Roy",
                           Email = "jason@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 18,
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
                           ACEID = "ACE6021",
                           FirstName = "Ravi",
                           LastName = "Bopara",
                           Email = "ravi@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 5,
                           DesignationId = 19,
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
                           ACEID = "ACE6022",
                           FirstName = "Chris",
                           LastName = "Gayle",
                           Email = "chris@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 20,
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
                           ACEID = "ACE6023",
                           FirstName = "Ravi",
                           LastName = "Rampaul",
                           Email = "rampaul@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 21,
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
                           ACEID = "ACE6024",
                           FirstName = "Andre",
                           LastName = "Russell",
                           Email = "russ@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 22,
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
                           ACEID = "ACE6025",
                           FirstName = "Darren",
                           LastName = "Sammy",
                           Email = "sammy@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 6,
                           DesignationId = 23,
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
                           ACEID = "ACE6026",
                           FirstName = "Kane",
                           LastName = "Williamson",
                           Email = "kane@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 24,
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
                           ACEID = "ACE6027",
                           FirstName = "Jimmy",
                           LastName = "Neesham",
                           Email = "jimmmy@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 25,
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
                           ACEID = "ACE6028",
                           FirstName = "Tom",
                           LastName = "Latham",
                           Email = "tom@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 26,
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
                           ACEID = "ACE6029",
                           FirstName = "Finn",
                           LastName = "Allen",
                           Email = "finn@tenacious.com",
                           Image=null,
                           ImageName=null,
                           Gender="Male",
                           DOB = DateTime.Now.AddDays(-9000),
                           OrganisationId = 3,
                           DepartmentId = 7,
                           DesignationId = 27,
                           ReportingPersonId = 26,
                           HRId = 24,
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
                            Reason = "Best Performer in Team",
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
                            Reason = "Best Performer in Team",
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
                            Reason = "Best Performer in Team",
                            RejectedReason = null,
                            CouponCode = null,
                            AddedBy = 6,
                            AddedOn = DateTime.Now,
                            UpdatedBy = 4,
                            UpdatedOn = DateTime.Now
                        },
                        
                    });
                    context?.SaveChanges();
                }
               
            }
        }
        
       
    }
}