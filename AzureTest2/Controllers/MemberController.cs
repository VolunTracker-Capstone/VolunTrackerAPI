using Microsoft.AspNetCore.Mvc;
using AzureTest2.Models;
using AzureTest2.Data; // Add this to use your DbContext
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AzureTest2.Controllers
{
    public class MemberController : ControllerBase
    {
        private readonly AzureTest2Context _context;

        public MemberController(AzureTest2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _context.Members.ToListAsync();
            return Ok(members);
        }
    }
}