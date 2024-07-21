using AspNetCoreCrudPractice.Data;
using AspNetCoreCrudPractice.Models;
using AspNetCoreCrudPractice.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCrudPractice.Controllers
{
    public class EmployeesController : Controller
    {


        //try implement repository pattern with Interface. 
        //try implement exception handling and logging to files.




        //create dbcontext constructor for this class:
        private readonly MVCDemoDbContext _dbcontext;

        public EmployeesController(MVCDemoDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _dbcontext.Employees.ToListAsync();
            return View(employees);
        }




        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {


            Employee employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };

            await _dbcontext.Employees.AddAsync(employee);
            await _dbcontext.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await _dbcontext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                UpdateEmployeeViewModel viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };

                //not sure what's happening here:
                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await _dbcontext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await _dbcontext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await _dbcontext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                _dbcontext.Employees.Remove(employee);
                await _dbcontext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}
