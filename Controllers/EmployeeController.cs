using DRAssessment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DRAssessment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DRDbContext _dbContext;

        public EmployeeController(DRDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var employees = _dbContext.Employees.ToList();
            return View("~/Views/employee/Index.cshtml", employees);
        }
        [HttpGet]
        public IActionResult VacationForm(int employeeId)
        {
            var employee = _dbContext.Employees.Find(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = new VacationRequest
            {
                EmployeeId = employee.EmployeeId,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };

            return View("~/Views/employee/VacationForm.cshtml", viewModel);
        }
        [HttpPost]
        public IActionResult VacationForm(VacationRequest viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View( viewModel);
            }

            var vacationRequest = new VacationRequest
            {
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                EmployeeId = viewModel.EmployeeId
            };

            _dbContext.VacationRequests.Add(vacationRequest);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
