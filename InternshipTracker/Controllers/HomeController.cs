using System.Diagnostics;
using InternshipTracker.Data;
using InternshipTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipTracker.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;
        var soon = today.AddDays(14);
        var vm = new DashBoardViewModel
        {
            Counts = await _context.JobApplications
                .GroupBy(a => a.Status)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key, x => x.Count),
            Upcoming = await _context.JobApplications
                .Where(a => a.Status == ApplicationStatus.Wishlist
                         && a.Deadline != null
                         && a.Deadline >= today
                         && a.Deadline <= soon)
                .OrderBy(a => a.Deadline)
                .ToListAsync()
        };
        return View(vm);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None,
NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ??
HttpContext.TraceIdentifier
        });
    }
}