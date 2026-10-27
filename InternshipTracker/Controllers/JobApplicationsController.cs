using System.Security.Claims;
using InternshipTracker.Data;
using InternshipTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace InternshipTracker.Controllers;
[Authorize]   // every action in this controller requires the user to be Lin
public class JobApplicationsController : Controller
{
    private readonly AppDbContext _context;
    public JobApplicationsController(AppDbContext context)
    {
        _context = context;
    }
    // The id of the logged-in user
    private string CurrentUserId =>
User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    // GET: JobApplications
    public async Task<IActionResult> Index(ApplicationStatus? status, string?
search)
    {
        var userId = CurrentUserId;
        var query = _context.JobApplications.Where(a => a.UserId == userId);
        // only my rows
        if (status.HasValue)
        {
            query = query.Where(a => a.Status == status.Value);
        }
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(a => a.Company.Contains(search) ||
a.Role.Contains(search));
        }
        return View(await query.OrderBy(a => a.Deadline).ToListAsync());
    }
    // GET: JobApplications/Details/5
    public async Task<IActionResult> Details(int? id)

    {
        if (id == null) return NotFound();
    var userId = CurrentUserId;
    var jobapplication = await _context.JobApplications
        .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (jobapplication == null) return NotFound();
        return View(jobapplication);
    }
    // GET: JobApplications/Create
    public IActionResult Create()
    {
        return View();
    }
    // POST: JobApplications/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>
Create([Bind("Company,Role,JobUrl,Status,DateApplied,Deadline,Notes")]
JobApplication jobapplication)
    {
        jobapplication.UserId = CurrentUserId;   // the owner comes from the login, never from the form
        if (ModelState.IsValid)
        {
            _context.Add(jobapplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(jobapplication);
    }
    // GET: JobApplications/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var userId = CurrentUserId;
        var jobapplication = await _context.JobApplications

            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (jobapplication == null) return NotFound();
        return View(jobapplication);
    }
    // POST: JobApplications/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id,
[Bind("Id,Company,Role,JobUrl,Status,DateApplied,Deadline,Notes")]
JobApplication jobapplication)
    {
        if (id != jobapplication.Id) return NotFound();
        // Load the real row and make sure it belongs to this user
        var userId = CurrentUserId;
        var existing = await _context.JobApplications
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (existing == null) return NotFound();
        if (ModelState.IsValid)
        {
            // If the deadline changed, allow a fresh reminder
            if (existing.Deadline != jobapplication.Deadline)
            {
                existing.ReminderSentOn = null;
            }
            existing.Company = jobapplication.Company;
            existing.Role = jobapplication.Role;
            existing.JobUrl = jobapplication.JobUrl;
            existing.Status = jobapplication.Status;
            existing.DateApplied = jobapplication.DateApplied;
            existing.Deadline = jobapplication.Deadline;
            existing.Notes = jobapplication.Notes;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(jobapplication);
    }
   
    // GET: JobApplications/Delete/5
    public async Task<IActionResult> Delete(int? id)
{
    if (id == null) return NotFound();
    var userId = CurrentUserId;
    var jobapplication = await _context.JobApplications
        .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    if (jobapplication == null) return NotFound();
    return View(jobapplication);
}
// POST: JobApplications/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int? id)
{
    var userId = CurrentUserId;
    var jobapplication = await _context.JobApplications
        .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    if (jobapplication != null)
    {
        _context.JobApplications.Remove(jobapplication);
        await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
}
}