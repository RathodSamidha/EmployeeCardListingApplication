using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeCardListingApplication.Data;
using EmployeeCardListingApplication.Models;
using EmployeeCardListingApplication.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Xml.Linq;


namespace EmployeeCardListingApplication.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public EmployeesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(m => m.EmployeeID == id);

                var employeeViewModel = new EmployeeViewModel()
                {
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name,
                    Department = employee.Department,
                    Designation = employee.Designation,
                    Phone = employee.Phone,
                    ExistingImage = employee.ProfilePicture
                };

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    string uniqueFileName = ProcessUploadedFile(model);
                    Employee employee = new()
                    {
                        EmployeeID = model.EmployeeID,
                        Name = model.Name,
                        Department = model.Department,
                        Designation = model.Designation,
                        Phone = model.Phone,                        
                        ProfilePicture = uniqueFileName
                    };

                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
               // }
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            var employeeViewModel = new EmployeeViewModel()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                Department = employee.Department,
                Designation = employee.Designation,
                Phone = employee.Phone,
                ExistingImage = employee.ProfilePicture
            };

            if (employee == null)
            {
                return NotFound();
            }
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            //if (ModelState.IsValid)
            //{
               var employee = await _context.Employees.FindAsync(model.Id);

                employee.Name = model.Name;
                employee.Department = model.Department;
                employee.Designation = model.Designation;
                employee.Phone = model.Phone;
                
                if (model.ProfilePicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(_environment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    employee.ProfilePicture = ProcessUploadedFile(model);
                }
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);

            var employeeViewModel = new EmployeeViewModel()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                Department = employee.Department,
                Designation = employee.Designation,
                Phone = employee.Phone,
                ExistingImage = employee.ProfilePicture
            };
            if (employee == null)
            {
                return NotFound();
            }

            return View(employeeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            //string deleteFileFromFolder = "wwwroot\\Uploads\\";
            string deleteFileFromFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFileFromFolder, employee.ProfilePicture);
            _context.Employees.Remove(employee);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }

        private string ProcessUploadedFile(EmployeeViewModel model)
        {
            string uniqueFileName = null;
            string path = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
  
}
