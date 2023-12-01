using EmployeeCardListingApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeCardListingApplication.Data
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee()
                        {
                          Name="Neil Rathod",
                          Designation="DEVELOPER",
                          Department="IT",
                          ProfilePicture="/avatar.png",
                          Phone="8830825615"
                        },
                        new Employee()
                        {
                          Name="Samidha Rathod",
                          Designation="DEVELOPER",
                          Department="IT",
                          ProfilePicture="/avatar.png",
                          Phone="8830825615"
                        },
                        new Employee()
                        {
                           Name="Purva Rathod",
                          Designation="DEVELOPER",
                          Department="IT",
                          ProfilePicture="/avatar.png",
                          Phone="8830825615"
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
