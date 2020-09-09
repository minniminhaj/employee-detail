using EmployeeDetail.Models;
using EmployeeDetail.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetail.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeRepository _employeeList;

        public IHostingEnvironment HostingEnvironment { get; }

        public HomeController(IEmployeeRepository employeeList, IHostingEnvironment hostingEnvironment)
        {
            _employeeList = employeeList;
            HostingEnvironment = hostingEnvironment;
        }
        public ViewResult Index()
        {
            var model = _employeeList.GetAllEmployees();
            return View(model);
        }
        public ViewResult Details(int Id )
        {
            var model = _employeeList.GetEmployee(Id);
            var homeDetailModel = new HomeDetailsViewModel()
            {
                Employee = model,
                PageTitle="This is the main page"
            };
            return View(homeDetailModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeList.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id=employee.Id,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath=employee.PhotoPath,

            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = FileUploadedProcess(model);
                
               Employee employee= _employeeList.GetEmployee(model.Id);
                employee.EmployeeName = model.EmployeeName;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(HostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = model.ExistingPhotoPath;
                }
               Employee UpdatedEmployee= _employeeList.Update(employee);

                return RedirectToAction("Index");
            }
            return View();

        }


        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = FileUploadedProcess(model);
                Employee newEmployee = new Employee
                {
                    EmployeeName = model.EmployeeName,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName,
                };
                _employeeList.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
            
        }

        private string FileUploadedProcess(EmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using(var fileStream=new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
