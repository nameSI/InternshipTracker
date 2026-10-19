using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternshipTracker.Models;
using InternshipTracker.Data;

public class JobApplicationsController : Controller
{
    private readonly AppDbContext _context;

    public JobApplicationsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: JOBAPPLICATIONS
    // GET: JobApplications
    public async Task<IActionResult> Index(ApplicationStatus? status)
    {
        var query = _context.JobApplications.AsQueryable();
        if (status.HasValue)
        {
            query = query.Where(a => a.Status == status.Value);
        }
        return View(await query.OrderBy(a => a.Deadline).ToListAsync());
    }

    // GET: JOBAPPLICATIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var jobapplication = await _context.JobApplications
            .FirstOrDefaultAsync(m => m.Id == id);
        if (jobapplication == null)
        {
            return NotFound();
        }

        return View(jobapplication);
    }

    // GET: JOBAPPLICATIONS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: JOBAPPLICATIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Company,Role,JobUrl,Status,DateApplied,Deadline,Notes")] JobApplication jobapplication)
    {
        if (ModelState.IsValid)
        {
            _context.Add(jobapplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(jobapplication);
    }

    // GET: JOBAPPLICATIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var jobapplication = await _context.JobApplications.FindAsync(id);
        if (jobapplication == null)
        {
            return NotFound();
        }
        return View(jobapplication);
    }

    // POST: JOBAPPLICATIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Company,Role,JobUrl,Status,DateApplied,Deadline,Notes")] JobApplication jobapplication)
    {
        if (id != jobapplication.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(jobapplication);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobApplicationExists(jobapplication.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(jobapplication);
    }

    // GET: JOBAPPLICATIONS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var jobapplication = await _context.JobApplications
            .FirstOrDefaultAsync(m => m.Id == id);
        if (jobapplication == null)
        {
            return NotFound();
        }

        return View(jobapplication);
    }

    // POST: JOBAPPLICATIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var jobapplication = await _context.JobApplications.FindAsync(id);
        if (jobapplication != null)
        {
            _context.JobApplications.Remove(jobapplication);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool JobApplicationExists(int? id)
    {
        return _context.JobApplications.Any(e => e.Id == id);
    }
}
