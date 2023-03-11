using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DRAssessment.Controllers
{
    public class OperationsController : Controller
    {
        private readonly DRDbContext _context;

        public OperationsController(DRDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var operations = await _context.Operations.ToListAsync();
            return View(operations);
        }
        public async Task<IActionResult> OperationDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.Name)
                .FirstOrDefaultAsync(o => o.OperationId == id);

            if (operation == null)
            {
                return NotFound();
            }

            return View("~/Views/operation/index.cshtml",operation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveVacationRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequests.FindAsync(id);

            if (vacationRequest == null)
            {
                return NotFound();
            }

            vacationRequest.Approval = "approved";
            _context.Update(vacationRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
