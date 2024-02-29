using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using trackingApi.Data;
using trackingApi.Models;

namespace trackingApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueDbContext _context;
        public IssueController(IssueDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Issue>> GetIssues()
        {
            return await _context.Issues.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetSingleIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound("Issue not found") : Ok(issue);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(Issue issue)
        {
            await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingleIssue), new { id = issue.Id }, issue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, Issue issue)
        {
            if (id != issue.Id) return BadRequest();
            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var issueToDelete = await _context.Issues.FindAsync(id);
            if(issueToDelete == null) return NotFound();

            _context.Issues.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
