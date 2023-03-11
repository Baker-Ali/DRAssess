using DRAssessment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DRAssessment.Controllers
{
    public class HeadOfDepartmentController : Controller
    {
        private readonly DRDbContext _context;
        public HeadOfDepartmentController(DRDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Include(e => e.Department).FirstOrDefault(e => e.EmployeeId == employeeId);

            var vacationRequests = _context.VacationRequests
                .Include(v => v.Employee)
                .Include(v => v.ApprovalHistories)
                .ThenInclude(a => a.HeadApprover)
                .Where(v => v.Employee.DepartmentId == employee.DepartmentId && v.HeadApproval == "pending");

            return View("~/Views/HeadOfDepartment/Index.cshtml", vacationRequests);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int? vacationRequestId)
        {
            if (vacationRequestId == null)
            {
                return NotFound();
            }

            var vacationRequest = _context.VacationRequests.FirstOrDefault(v => v.VacationRequestId == vacationRequestId);


            vacationRequest.HeadApproval = vacationRequest.HeadApproval == "approved" ? "pending" : "approved";

            var approver = _context.Employees.FirstOrDefault(e => e.EmployeeId == User.Identity.GetUserId());
            var approvalHistory = new ApprovalHistory
            {
                VacationRequest = vacationRequest,
                HeadApprover = approver,
                HeadApprovalDateTime = DateTime.Now,
                HeadApprovalStatus = vacationRequest.HeadApproval
            };

            vacationRequest.ApprovalHistories.Add(approvalHistory);
            _context.SaveChanges();

            return RedirectToAction("Index", new { employeeId = vacationRequest.EmployeeId });
        }
    }
}
