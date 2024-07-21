using AspNetCoreCrudPractice.Data;
using AspNetCoreCrudPractice.Models.DomainModels;
using AspNetCoreCrudPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCrudPractice.Controllers
{
    public class EmployeesDTController : Controller
    {

        private readonly MVCDemoDbContext _dbcontext;


        public EmployeesDTController(MVCDemoDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }





        public async Task<IActionResult> AddDT()
        {
            var employees = await _dbcontext.Employees.ToListAsync();
            return View(employees);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {

            //converting viewmodel to model?

            Employee employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };

            //dbcontext add and then save changes
            await _dbcontext.Employees.AddAsync(employee);
            await _dbcontext.SaveChangesAsync();


            return RedirectToAction("AddDT");
        }
    }
}
