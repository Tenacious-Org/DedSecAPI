using A5.Authentication;
using A5.Models;
using Microsoft.AspNetCore.Identity;

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
                            RoleID = 1,
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
                            RoleID = 2,
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
                            RoleID = 2,
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
                            RoleID = 2,
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
                            RoleID = 3,
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
                            RoleID = 4,
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
                            RoleID = 5,
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
                if(!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<Roles>(){
                        new Roles()
                        {
                            RoleName = "Admin"
                        },
                        new Roles()
                        {
                            RoleName = "Publisher"
                        },
                        new Roles()
                        {
                            RoleName = "Approver"
                        },
                        new Roles()
                        {
                            RoleName = "Requester"
                        },
                        new Roles()
                        {
                            RoleName = "User"
                        },
                    });
                }

            }
        }
        
        public static async Task SeedRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.Publisher))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Publisher));

                if (!await roleManager.RoleExistsAsync(UserRoles.Approver))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Approver));

                if (!await roleManager.RoleExistsAsync(UserRoles.Requester))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Requester));
                
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            };
        }
    }
}